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
    class Program
    {
        static void Main(string[] args)
        {
            var genericDao = new GenericDAO();

            var files = new string[] { "dataMM", "dataAU", "dataCA", "dataLN", "dataNZ", "dataUS" };
            foreach (var file in files)
            {



                var companyListFile = File.ReadAllText(@"C:\Users\User01\source\repos\rpinto1\academy-2021-07-kcs\lasagna\Rui\JsonFiles\" + file + ".txt");
                var jsonCompanyList = JObject.Parse(companyListFile);
                var companyArray = jsonCompanyList["Sheet1"];
                var companyList = companyArray.Children().ToList();
                Console.WriteLine(companyList.Count());
                var CompanyObject = new List<Company>();
                foreach (var item in companyList)
                {

                    Console.WriteLine(item["Name"]);
                    Console.WriteLine(item["Ticker"]);
                    Console.WriteLine(item["SectorId"]);
                    Console.WriteLine(item["Currency"]);
                    Console.WriteLine(item["Description"]);
                    Console.WriteLine(item["CountryId"]);
                    Console.WriteLine(item["Symbol"]);
                    Console.WriteLine(item["ExchangeId"]);
                    Console.WriteLine(item["CompanyType"]);
                    CompanyObject.Add(new Company
                    {
                        Name = item["Name"].ToString(),
                        Ticker = item["Ticker"].ToString(),
                        IndustryId = int.Parse(item["SectorId"].ToString()),
                        Currency = item["Currency"].ToString(),
                        Description = item["Description"].ToString(),
                        CountryId = int.Parse(item["CountryId"].ToString()),
                        Symbol = item["Symbol"].ToString(),
                        ExchangeId = int.Parse(item["ExchangeId"].ToString()),
                        CompanyType = item["CompanyType"].ToString(),
                        Uuid = Guid.NewGuid()
                    });
                    
                }

                 genericDao.AddRange<Company>(CompanyObject);
            }



            // API Requests to populate industry and sub Industry

            //Console.WriteLine("Enter user api Key");
            //string apiKey = Console.ReadLine();

            //var clientClass = new Client();

            //IRestResponse responseList = clientClass.GetAll("https://public-api.quickfs.net/v1/companies?api_key="+apiKey);

            //var responseCompanyList = JObject.Parse(responseList.Content)["data"];


            //for (int i = 1; i < responseCompanyList.ToObject<string[]>().Length; i++)
            //{


            //    if (clientClass.CheckQuota(apiKey) < 2000)
            //    {

            //        Environment.Exit(0);
            //    }

            //    IRestResponse response = clientClass.GetAll("https://public-api.quickfs.net/v1/data/all-data/" + responseCompanyList[i].ToString() + "?api_key=" + apiKey);

            //    var responseJson = JObject.Parse(response.Content);
            //    var metadata = responseJson["data"]["metadata"];
            //    Console.WriteLine(metadata["name"].ToString());

            //    Console.WriteLine(metadata["sector"].ToString());
            //    int industryId = 0;

            //    if (metadata["sector"] == null)
            //    {
            //        continue;
            //    }

            //    if (genericDao.Get(metadata["sector"].ToString()) == null)
            //    {
            //        var index = genericDao.Add<Industry>(new Industry
            //        {
            //            Name = metadata["sector"].ToString(),
            //            Uuid = Guid.NewGuid()
            //        });

            //        industryId = index.Id;
            //        Console.WriteLine("Insert Industry");
            //    }
            //    else
            //    {
            //        industryId = int.Parse(genericDao.Get(metadata["sector"].ToString()).Id.ToString());
            //    }

            //    if(metadata["industry"] == null)
            //    {
            //        continue;
            //    }
            //    if (genericDao.GetSub(metadata["industry"].ToString()) == null)
            //    {
            //        genericDao.Add<SubIndustry>(new SubIndustry
            //        {
            //            IndustryId = industryId,
            //            Name = metadata["sindustry"].ToString(),
            //            Uuid = Guid.NewGuid()
            //        });
            //        Console.WriteLine("Insert Sub");

            //    }




            //}





        }
    }
}
