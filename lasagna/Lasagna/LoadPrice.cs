using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.DataAccess;
using Newtonsoft.Json.Linq;
using Rui;
using System;
using System.Collections.Generic;

namespace Lasagna
{
    public class LoadPrice
    {

        public void LoadPrices()
        {

            var clientClass = new Client();

            var genericDao = new GenericDAO();
            var searchDao = new SearchDAO();

            var companyBD = genericDao.GetAll<Company>();
            var counting = 0;
            var queryString = "";

            var listUpdatedCompanys = new List<Company>();
            for (int companyIndex = 0; companyIndex < companyBD.Count; companyIndex++)
            {
                var company = companyBD[companyIndex];
                queryString += company.Symbol + ",";

                if (companyIndex % 199==0 || companyIndex == (companyBD.Count -1))
                {
                    var response = clientClass.GetAll("https://query1.finance.yahoo.com/v7/finance/quote?symbols=" + queryString);
                    var responseObject = JObject.Parse(response.Content)["quoteResponse"]["result"];
                    for (int responseIndex = 0; responseIndex < 200; responseIndex++)
                    {
                    var indexAddedDailyInfo = genericDao.Add<DailyInfo>( new DailyInfo { StockPrice = (decimal) responseObject[responseIndex]["regularMarketPreviousClose"] });
                        // ADD
                        //regularMarketPreviousClose
                        //epsTrailingTwelveMonths
                        //forwardPE
                        //
                        company.DailyInfo = indexAddedDailyInfo;
                        listUpdatedCompanys.Add(company);

                    }

                    counting = 0;
                    continue;
                }


                counting++;
            }

            genericDao.UpdateRange(listUpdatedCompanys);
        }
    }
}
