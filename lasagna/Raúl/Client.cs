using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rui
{
    class Client
    {


        public IRestResponse GetAll(string http)
        {

            var client = new RestClient(http);
            var request = new RestRequest(Method.GET);
            //request.AddHeader("x-rapidapi-key", "239cfb2611mshae42117f6fb66dfp13439djsn1beb14b99b16");
            //request.AddHeader("x-rapidapi-host", "yahoo-finance15.p.rapidapi.com");
           return client.Execute(request);

        }

        public int CheckQuota(string apiKey)
        {
            IRestResponse responseQuota = GetAll("https://public-api.quickfs.net/v1/usage?api_key=" + apiKey);

            var responseQuotaNumber = JObject.Parse(responseQuota.Content)["usage"]["quota"]["remaining"];

            Console.WriteLine(responseQuotaNumber.ToString());

            return int.Parse(responseQuotaNumber.ToString());


        }


    }
}
