using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Business
{
    public class ExternalServicesBO : IExternalServicesBO
    {
        

        //refactorizar a implementação
        //devolver um OperationResult<string>
        //utilizar o ExecuteOperation
		public string FetchGainLoseData()
        {
            //return ExecuteOperation(() =>
            //{
            //    //código da implementação
            //});


            var YAHOO_API_OPTIONS = new
            {
                key = "c16e3abf67msh98c7e364d5eed9ep19f991jsnd7123a9386c2",
                host = "yahoo-finance15.p.rapidapi.com"
            };

            
            //main url
			var yahooClient = new RestClient("https://yahoo-finance15.p.rapidapi.com/api/yahoo/");

			//endpoints
			var gainersRequest = new RestRequest("co/collections/day_gainers?start=0", DataFormat.Json);
			var losersRequest = new RestRequest("co/collections/day_losers?start=0", DataFormat.Json);

			//headers
			gainersRequest.AddHeader("x-rapidapi-key", YAHOO_API_OPTIONS.key);
			gainersRequest.AddHeader("x-rapidapi-host", YAHOO_API_OPTIONS.host);
			losersRequest.AddHeader("x-rapidapi-key", YAHOO_API_OPTIONS.key);
			losersRequest.AddHeader("x-rapidapi-host", YAHOO_API_OPTIONS.host);

			//request execution
			var gainersResponse = yahooClient.Get(gainersRequest);
			var losersResponse = yahooClient.Get(losersRequest);


            //error handling
            if (!gainersResponse.IsSuccessful || !losersResponse.IsSuccessful)
            {
                var obj = new { gainers = gainersResponse.ErrorMessage };

                return JsonConvert.SerializeObject(obj);
                
                //return "{ \n \"gainers\": " + gainersResponse.ErrorMessage + ", \n \"losers\": " + losersResponse.ErrorMessage + "\n }";

            }

            //sucessful result
            var result = JsonConvert.SerializeObject(gainersResponse, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );

			return result;
        }
    }
}
