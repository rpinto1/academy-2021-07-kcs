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
    class CompanyIndexCode
    {
        public void InsertCompanies(GenericDAO genericDao)
        {
            var file = "dataCompanyIndex";



                var companyListFile = File.ReadAllText(@"C:\Users\User01\source\repos\rpinto1\academy-2021-07-kcs\lasagna\Rui\JsonFiles\" + file + ".txt");
                var jsonCompanyList = JObject.Parse(companyListFile);
                var companyArray = jsonCompanyList["Sheet1"];
                var companyList = companyArray.Children().ToList();
                Console.WriteLine(companyList.Count());
                var CompanyObject = new List<CompanyIndex>();
                foreach (var item in companyList)
                {

                    Console.WriteLine(item["CompanyID"]);
                    Console.WriteLine(item["IndexID"]);

                    CompanyObject.Add(new CompanyIndex
                    {
                        CompanyId = int.Parse(item["CompanyID"].ToString()) ,
                        IndexId = int.Parse(item["IndexID"].ToString()) 

                    });

                }

                //genericDao.AddRange<CompanyIndex>(CompanyObject);
            
        }
        }
}
