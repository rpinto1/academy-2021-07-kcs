using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Business.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Business
{
    public class ExternalServicesBO : IExternalServicesBO
    {

        
        public async Task<IRestResponse> FetchDataAsync(string url, string endpoint, string keyHeader, string key, string hostHeader, string host, DataFormat format)
        {
            var client = new RestClient(url);

            var request = new RestRequest(endpoint, format);

            request.AddHeader(keyHeader, key);
            request.AddHeader(hostHeader, host);

            var responseAsync = client.GetAsync<IRestResponse>(request);
            
            var response = await responseAsync;

            return response;

        }

        public IRestResponse FetchData(string url, string endpoint, string keyHeader, string key, string hostHeader, string host, DataFormat format)
        {
            var client = new RestClient(url);

            var request = new RestRequest(endpoint, format);

            request.AddHeader(keyHeader, key);
            request.AddHeader(hostHeader, host);
            
            var response = client.Get<IRestResponse>(request);
           
            
            return response;

        }

        public IRestResponse FetchData(string url, string endpoint, string keyHeader, string key, DataFormat format)
        {
            var client = new RestClient(url);

            var request = new RestRequest(endpoint, format);

            request.AddHeader(keyHeader, key);
            
            var response = client.Get<IRestResponse>(request);


            return response;

        }

        public GenericReturn<string> FetchGainLoseData()
        {
            
            var url = "https://yahoo-finance15.p.rapidapi.com/api/yahoo/";

            var yahooAPIOptions = new
            {
                key = "c16e3abf67msh98c7e364d5eed9ep19f991jsnd7123a9386c2",
                host = "yahoo-finance15.p.rapidapi.com"
            };

            var endpoints = new
            {
                gainers = "co/collections/day_gainers?start=0",
                losers = "co/collections/day_losers?start=0"
            };


            return ExecuteOperation(() =>
            {
                
                var gainersResponse = FetchData(url, endpoints.gainers, "x-rapidapi-key", yahooAPIOptions.key, "x-rapidapi-host", yahooAPIOptions.host, DataFormat.Json);
                var losersResponse = FetchData(url, endpoints.losers, "x-rapidapi-key", yahooAPIOptions.key, "x-rapidapi-host", yahooAPIOptions.host, DataFormat.Json);

                //if (!gainersResponse.IsSuccessful) throw gainersResponse.ErrorException;
                //if (!losersResponse.IsSuccessful) throw losersResponse.ErrorException;

                var resultObject = new
                {
                    gainers = JsonConvert.DeserializeObject<GainLoseResponse>(gainersResponse.Content).Quotes,
                    losers = JsonConvert.DeserializeObject<GainLoseResponse>(losersResponse.Content).Quotes
                };

                return JsonConvert.SerializeObject(resultObject);
                
            });
            
        }

        //deve ser refactorizado
        //add overloaded methods
        //remover e colocar logica na classe generica

        public GenericReturn<string> FetchNewsData()
        {

            var url = "https://newsapi.org/";

            var newsAPIOptions = new
            {
                key = "1d46c2ebccbd48e597c869e5881a2d87",
            };

            var endpoint = "v2/top-headlines?country=${location}&category=business&pageSize=5";


            return ExecuteOperation(() =>
            {

                var newsResponse = FetchData(url, endpoint, "apiKey", newsAPIOptions.key, DataFormat.Json);
                
                var resultObject = new
                {
                    news = JsonConvert.DeserializeObject<GainLoseResponse>(newsResponse.Content)
                };

                return JsonConvert.SerializeObject(resultObject);

            });

        }

        GenericReturn<string> ExecuteOperation(Func<string> lambda)
        {
            var operationResult = new GenericReturn<string>();
            
            try
            {
                operationResult.Succeeded = true;
                operationResult.Result = lambda.Invoke();
            }
            catch (Exception e)
            {
                operationResult.Succeeded = false;
                operationResult.Message = "[ExecuteOperation] An operation error has occured: " + e.Message;
            }

            return operationResult;
        }
    }
}
