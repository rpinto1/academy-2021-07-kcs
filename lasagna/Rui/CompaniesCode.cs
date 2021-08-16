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
    class CompaniesCode
    {
        public void InsertCompanies(GenericDAO genericDao)
        {
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
                    Console.WriteLine(item["IndustryId"]);
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
                        SectorId = int.Parse(item["SectorId"].ToString()),
                        IndustryId = int.Parse(item["IndustryId"].ToString()),
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
        }
    }
}
