using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Business.Pocos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
             
        
        Task<IRestResponse> FetchDataAsync(string url, string endpoint, string keyHeader, string key, DataFormat format)
        {
            var client = new RestClient(url);

            var request = new RestRequest(endpoint, format);

            request.AddHeader(keyHeader, key);

            var responseAsync = client.ExecuteGetAsync(request);

            return responseAsync;
        }

        
        public Task<GenericReturn<GainLosePoco>> FetchGainLoseData()
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

                var result = new GainLosePoco
                {
                    Gainers = new GainLoseResponse { Quotes = JObject.Parse(gainersResponse.Content)["quotes"].ToObject<IEnumerable<GainLoseCompanyData>>() },
                    Losers = new GainLoseResponse { Quotes = JObject.Parse(losersResponse.Content)["quotes"].ToObject<IEnumerable<GainLoseCompanyData>>() }
                };

                return result;
            });
        }

        
        public Task<GenericReturn<NewsPoco>> FetchNewsData()
        {

            var url = "https://newsapi.org/";

            var newsAPIKey = "1d46c2ebccbd48e597c869e5881a2d87";

            var country = "us";

            var endpoint = "v2/top-headlines?country=" + country + "&category=business&pageSize=5";


            return _genericBusinessLogic.ExecuteOperation(async () =>
            {
                var newsResponse = await FetchDataAsync(url, endpoint, "x-api-key", newsAPIKey, DataFormat.Json);

                var resultObject = JsonConvert.DeserializeObject<GainLoseResponse>(newsResponse.Content);

                var result = new NewsPoco { Articles = JObject.Parse(newsResponse.Content)["articles"].ToObject<IEnumerable<NewsData>>() };

                return result;
            });
        }

    }
}
