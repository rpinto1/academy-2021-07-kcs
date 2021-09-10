using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.DataAccess;
using Newtonsoft.Json.Linq;
using Rui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                if (company.YahooTicker == null)
                {
                    continue;
                }
                
                queryString += HttpUtility.UrlEncode(company.YahooTicker)  + ",";

                if ((counting % 199==0 && counting > 0)|| companyIndex == (companyBD.Count -1))
                {
                    var response = clientClass.GetAll("https://query1.finance.yahoo.com/v7/finance/quote?symbols=" + queryString);
                    var responseObject = JObject.Parse(response.Content)["quoteResponse"]["result"];
                    var countingList = responseObject.Children().ToList().Count;
                    Console.WriteLine(queryString);
                    Console.WriteLine("----------");

                    for (int responseIndex = 0; responseIndex < countingList; responseIndex++)
                    {

                    var indexAddedDailyInfo = genericDao.Add<DailyInfo>( new DailyInfo {EpsTTM = (decimal) (responseObject[responseIndex]["epsTrailingTwelveMonths"] ?? 0 ),
                                                                                        ForwardPe = (decimal) (responseObject[responseIndex]["forwardPE"]?? (responseObject[responseIndex]["trailingPE"]?? 0)) ,
                                                                                        StockPrice = (decimal) (responseObject[responseIndex]["regularMarketPreviousClose"]??0 )});
                        // ADD
                        //regularMarketPreviousClose
                        //epsTrailingTwelveMonths
                        //forwardPE
                        //
                        var companyIndexSecondary = companyBD.FindIndex(x => (x.YahooTicker?? "" ).ToLower() == ( (responseObject[responseIndex]["symbol"])).ToString().ToLower());
                        companyBD[companyIndexSecondary].DailyInfo = indexAddedDailyInfo;
                        listUpdatedCompanys.Add(companyBD[companyIndexSecondary]);

                    }

                    counting = 0;
                    queryString = "";
                    continue;
                }


                counting++;
            }

            genericDao.UpdateRange(listUpdatedCompanys);
        }
    }
}
