using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Business.Pocos;
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
        private IGenericBusinessLogic _genericBusinessLogic;


        public ExternalServicesBO(IGenericBusinessLogic genericBusinessLogic)
        {
            _genericBusinessLogic = genericBusinessLogic;
        }
             
        
        async Task<IRestResponse> FetchDataAsync(string url, string endpoint, string keyHeader, string key, DataFormat format)
        {
            var client = new RestClient(url);

            var request = new RestRequest(endpoint, format);

            request.AddHeader(keyHeader, key);

            var responseAsync = client.GetAsync<IRestResponse>(request);

            var response = await responseAsync;

            return response;

        }


        public Task<GenericReturn<string>> FetchGainLoseData()
        {
            
            var url = "https://yahoo-finance15.p.rapidapi.com/api/yahoo/";

            var yahooAPIKey = "c16e3abf67msh98c7e364d5eed9ep19f991jsnd7123a9386c2";

            var endpoints = new
            {
                gainers = "co/collections/day_gainers?start=0",
                losers = "co/collections/day_losers?start=0"
            };

            return _genericBusinessLogic.ExecuteOperation(async () =>
            {
                
                var gainersResponse = await FetchDataAsync(url, endpoints.gainers, "x-rapidapi-key", yahooAPIKey, DataFormat.Json);
                var losersResponse = await FetchDataAsync(url, endpoints.losers, "x-rapidapi-key", yahooAPIKey, DataFormat.Json);

                var resultObject = new
                {
                    gainers = JsonConvert.DeserializeObject<GainLoseResponse>(gainersResponse.Content).Quotes,
                    losers = JsonConvert.DeserializeObject<GainLoseResponse>(losersResponse.Content).Quotes
                };

                return JsonConvert.SerializeObject(resultObject);
                
            });
        }

        
        public Task<GenericReturn<string>> FetchNewsData()
        {

            var url = "https://newsapi.org/";

            var newsAPIKey = "1d46c2ebccbd48e597c869e5881a2d87";

            var endpoint = "v2/top-headlines?country=${location}&category=business&pageSize=5";



            return _genericBusinessLogic.ExecuteOperation(async () =>
            {
                var newsResponse = await FetchDataAsync(url, endpoint, "apiKey", newsAPIKey, DataFormat.Json);

                var resultObject = JsonConvert.DeserializeObject<GainLoseResponse>(newsResponse.Content);

                return JsonConvert.SerializeObject(resultObject);


            });

        }


        //to remove
        //GenericReturn<string> ExecuteOperation(Func<string> lambda)
        //{
        //    var operationResult = new GenericReturn<string>();

        //    try
        //    {
        //        operationResult.Succeeded = true;
        //        operationResult.Result = lambda();
        //    }
        //    catch (Exception e)
        //    {
        //        operationResult.Succeeded = false;
        //        operationResult.Message = "[ExecuteOperation] An operation error has occured: " + e.Message;
        //    }

        //    return operationResult;
        //}
        //IRestResponse FetchData(string url, string endpoint, string keyHeader, string key, string hostHeader, string host, DataFormat format)
        //{
        //    var client = new RestClient(url);

        //    var request = new RestRequest(endpoint, format);

        //    request.AddHeader(keyHeader, key);
        //    request.AddHeader(hostHeader, host);
        //    request
        //    var response = client.Get<IRestResponse>(request);


        //    return response;

        //}

        //IRestResponse FetchData(string url, string endpoint, string keyHeader, string key, DataFormat format)
        //{
        //    var client = new RestClient(url);

        //    var request = new RestRequest(endpoint, format);

        //    request.AddHeader(keyHeader, key);

        //    var response = client.Get<IRestResponse>(request);


        //    return response;

        //}

        //async Task<IRestResponse> FetchDataAsync(string url, string endpoint, string keyHeader, string key, string hostHeader, string host, DataFormat format)
        //{
        //    var client = new RestClient(url);

        //    var request = new RestRequest(endpoint, format);

        //    request.AddHeader(keyHeader, key);
        //    request.AddHeader(hostHeader, host);

        //    var responseAsync = client.GetAsync<IRestResponse>(request);

        //    var response = await responseAsync;

        //    return response;

        //}

    }
}
