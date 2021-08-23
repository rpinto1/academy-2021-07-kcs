using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Text;
using KCSit.SalesforceAcademy.Lasagna.DataAccess;
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
    class Industries
    {
        public void InsertIndustries(GenericDAO genericDao)
        {

            var files = new string[] { "dataMM", "dataAU", "dataCA", "dataLN", "dataNZ", "dataUS" };
            var industryList = new List<Industry>();
            var industryMap = new Dictionary<string, int>();

            foreach (var file in files)
            {
                var companyListFile = File.ReadAllText(@"C:\Users\User01\source\repos\rpinto1\academy-2021-07-kcs\lasagna\Rui\JsonFiles\" + file + ".txt");
                var jsonCompanyList = JObject.Parse(companyListFile);
                var companyArray = jsonCompanyList["Sheet1"];
                var companyList = companyArray.Children().ToList();

                foreach (var item in companyList)
                {

                    if (item["Industry"] == null)
                    {
                        continue;
                    }
                    if (item["Industry"].ToString().Equals("0") || item["Industry"].ToString().Equals("Unclassified"))
                    {
                        continue;
                    }

                    if (!industryMap.ContainsKey(item["Industry"].ToString()))
                    {
                        industryMap.Add(item["Industry"].ToString(), int.Parse(item["SectorId"].ToString()));
                        industryList.Add(new Industry
                        {
                            SectorId = int.Parse(item["SectorId"].ToString()),
                            Name = item["Industry"].ToString(),
                            Uuid = Guid.NewGuid()
                        });
                    }

                }
                

            }
            genericDao.AddRange<Industry>(industryList);
        }
    }
}
