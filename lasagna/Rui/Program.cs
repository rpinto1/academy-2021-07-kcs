using KCSit.SalesforceAcademy.Kappify.DataAccess;
using KCSit.SalesforceAcademy.Lasagna.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;

namespace Rui
{
    class Program
    {
        static void Main(string[] args)
        {
            var client2 = new RestClient("https://public-api.quickfs.net/v1/companies?api_key=d4089a95fc589f2d804c241f4f23b9732ff9ab6e");
            var request2 = new RestRequest(Method.GET);
            //request.AddHeader("x-rapidapi-key", "239cfb2611mshae42117f6fb66dfp13439djsn1beb14b99b16");
            //request.AddHeader("x-rapidapi-host", "yahoo-finance15.p.rapidapi.com");
            IRestResponse responseList = client2.Execute(request2);

            var responseCompanyList = JObject.Parse(responseList.Content)["data"];

            for (int i = 700; i < responseCompanyList.ToObject<string[]>().Length; i++)
            {


                

                var quota = new RestClient("https://public-api.quickfs.net/v1/usage?api_key=d4089a95fc589f2d804c241f4f23b9732ff9ab6e");
                var requestQuota = new RestRequest(Method.GET);
                //request.AddHeader("x-rapidapi-key", "239cfb2611mshae42117f6fb66dfp13439djsn1beb14b99b16");
                //request.AddHeader("x-rapidapi-host", "yahoo-finance15.p.rapidapi.com");
                IRestResponse responseQuota = quota.Execute(requestQuota);

                var responseQuotaNumber = JObject.Parse(responseQuota.Content)["usage"]["quota"]["remaining"];

                Console.WriteLine(responseQuotaNumber.ToString());

                if (int.Parse(responseQuotaNumber.ToString()) <= 2000)
                {
                    
                    Environment.Exit(0);
                }


                var client = new RestClient("https://public-api.quickfs.net/v1/data/all-data/" + responseCompanyList[i].ToString() + "?api_key=d4089a95fc589f2d804c241f4f23b9732ff9ab6e");
                var request = new RestRequest(Method.GET);
                //request.AddHeader("x-rapidapi-key", "239cfb2611mshae42117f6fb66dfp13439djsn1beb14b99b16");
                //request.AddHeader("x-rapidapi-host", "yahoo-finance15.p.rapidapi.com");
                IRestResponse response = client.Execute(request);

                var responseJson = JObject.Parse(response.Content);
                var metadata = responseJson["data"]["metadata"];
                Console.WriteLine(metadata["name"].ToString());

                Console.WriteLine(metadata["industry"].ToString());
                int industryId = 0;
                var genericDao = new GenericDAO();
                if (genericDao.Get(metadata["industry"].ToString()) == null)
                {
                    var index = genericDao.Add<Industry>(new Industry
                    {
                        Name = metadata["industry"].ToString(),
                        Uuid = Guid.NewGuid()
                    });

                    industryId = index.Id;
                    Console.WriteLine("Insert Industry");
                }
                else
                {
                    industryId = int.Parse(genericDao.Get(metadata["industry"].ToString()).Id.ToString());
                }

                if(metadata["subindustry"] == null)
                {
                    continue;
                }
                if (genericDao.GetSub(metadata["subindustry"].ToString()) == null)
                {
                    genericDao.Add<SubIndustry>(new SubIndustry
                    {
                        IndustryId = industryId,
                        Name = metadata["subindustry"].ToString(),
                        Uuid = Guid.NewGuid()
                    });
                    Console.WriteLine("Insert Sub");

                }




            }



            

        }
    }
}
