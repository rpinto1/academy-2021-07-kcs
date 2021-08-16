using System;
using System.Collections.Generic;
using System.Text;
using KCSit.SalesforceAcademy.Kappify.DataAccess;
using KCSit.SalesforceAcademy.Lasagna.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rui
{
    class SubIndustries
    {

        public void GetSubIndustries(GenericDAO genericDao)
        {


            //API Requests to populate industry and sub Industry

            Console.WriteLine("Enter user api Key");
            string apiKey = Console.ReadLine();

            var clientClass = new Client();

            IRestResponse responseList = clientClass.GetAll("https://public-api.quickfs.net/v1/companies?api_key=" + apiKey);

            var responseCompanyList = JObject.Parse(responseList.Content)["data"];


            for (int i = 1; i < responseCompanyList.ToObject<string[]>().Length; i++)
            {


                if (clientClass.CheckQuota(apiKey) < 2000)
                {

                    Environment.Exit(0);
                }

                IRestResponse response = clientClass.GetAll("https://public-api.quickfs.net/v1/data/all-data/" + responseCompanyList[i].ToString() + "?api_key=" + apiKey);

                var responseJson = JObject.Parse(response.Content);
                var metadata = responseJson["data"]["metadata"];
                Console.WriteLine(metadata["name"].ToString());

                Console.WriteLine(metadata["sector"].ToString());
                int industryId = 0;

                if (metadata["sector"] == null)
                {
                    continue;
                }

                if (genericDao.Get(metadata["sector"].ToString()) == null)
                {
                    var index = genericDao.Add<Industry>(new Industry
                    {
                        Name = metadata["sector"].ToString(),
                        Uuid = Guid.NewGuid()
                    });

                    industryId = index.Id;
                    Console.WriteLine("Insert Industry");
                }
                else
                {
                    industryId = int.Parse(genericDao.Get(metadata["sector"].ToString()).Id.ToString());
                }

                if (metadata["industry"] == null)
                {
                    continue;
                }
                if (genericDao.GetSub(metadata["industry"].ToString()) == null)
                {
                    genericDao.Add<SubIndustry>(new SubIndustry
                    {
                        IndustryId = industryId,
                        Name = metadata["sindustry"].ToString(),
                        Uuid = Guid.NewGuid()
                    });
                    Console.WriteLine("Insert Sub");

                }


            }
        }

    }
}
