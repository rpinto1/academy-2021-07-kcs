using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.DataAccess;
using Newtonsoft.Json.Linq;
using Rui;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Lasagna
{
    class UpdatePrice
    {

        public void UpdatePrices()
        {

            var clientClass = new Client();

            var genericDao = new GenericDAO();
            var searchDao = new SearchDAO();

            var companyBD = genericDao.GetAll<Company>();
            var companyDaily = genericDao.GetAll<DailyInfo>();
            var counting = 0;
            var queryString = "";

            var listUpdatedDailyInfo = new List<DailyInfo>();
            for (int companyIndex = 0; companyIndex < companyBD.Count; companyIndex++)
            {
                var company = companyBD[companyIndex];
                if (company.YahooTicker == null)
                {
                    continue;
                }

                queryString += HttpUtility.UrlEncode(company.YahooTicker) + ",";

                if ((counting % 199 == 0 && counting > 0) || companyIndex == (companyBD.Count - 1))
                {
                    var response = clientClass.GetAll("https://query1.finance.yahoo.com/v7/finance/quote?symbols=" + queryString);
                    var responseObject = JObject.Parse(response.Content)["quoteResponse"]["result"];
                    var countingList = responseObject.Children().ToList().Count;
                    Console.WriteLine(queryString);
                    Console.WriteLine("----------");

                    for (int responseIndex = 0; responseIndex < countingList; responseIndex++)
                    {
                        try
                        {
                            var companyIndexSecondary = companyBD.FindIndex(x => (x.YahooTicker ?? "").ToLower() == ((responseObject[responseIndex]["symbol"])).ToString().ToLower());
                            var dailyInfoId = companyBD[companyIndexSecondary].DailyInfoId.Value;
                            var daily = companyDaily.Find(x => x.Id == dailyInfoId);
                            daily.PreviousClose = daily.StockPrice;
                            daily.EpsTTM = (decimal)(responseObject[responseIndex]["epsTrailingTwelveMonths"] ?? 0);
                            daily.ForwardPe = (decimal)(responseObject[responseIndex]["forwardPE"] ?? (responseObject[responseIndex]["trailingPE"] ?? 0));
                            daily.StockPrice = (decimal)(responseObject[responseIndex]["regularMarketPreviousClose"] ?? 0);

                            // ADD
                            //regularMarketPreviousClose
                            //epsTrailingTwelveMonths
                            //forwardPE
                            //
                            listUpdatedDailyInfo.Add(daily);
                        }
                        catch (Exception ex)
                        {

                            WriteResult(ex.Message);
                        }

                    }

                    counting = 0;
                    queryString = "";
                    continue;
                }


                counting++;
            }
            genericDao.UpdateRange<DailyInfo>(listUpdatedDailyInfo);

        }
        public bool WriteResult(string result)
        {
            try
            {
                using (StreamWriter sr = File.AppendText(@"C:\Users\User01\source\repos\rpinto1\academy-2021-07-kcs\lasagna\Lasagna\result.txt"))
                {
                    sr.WriteLine(result);
                    sr.Flush();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }


        }
    }


}
