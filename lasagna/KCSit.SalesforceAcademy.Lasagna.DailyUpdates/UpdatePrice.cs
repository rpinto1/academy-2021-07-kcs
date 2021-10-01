using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.DataAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace KCSit.SalesforceAcademy.Lasagna.DailyUpdates
{

    public class UpdatePrice
    {
        Client _clientClass = new Client();

        GenericDAO _genericDao = new GenericDAO();


        public async Task UpdatePrices()
        {


            var companyJson = File.ReadAllText(@".\listQFSUpdate.json");

            var companyBD1 = JsonConvert.DeserializeObject<List<Company>>(companyJson);
            var companyBD = await _genericDao.GetAllAsync<Company>();
            var companyDaily = await _genericDao.GetAllAsync<DailyInfo>();
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
                if (companyBD1.FindIndex(element => element.Id == company.Id) > 0)
                {
                    continue;
                }

                queryString += HttpUtility.UrlEncode(company.YahooTicker) + ",";

                if ((counting % 199 == 0 && counting > 0) || companyIndex == (companyBD.Count - 1))
                {
                    var response = _clientClass.GetAll("https://query1.finance.yahoo.com/v7/finance/quote?symbols=" + queryString);
                    var responseObject = JObject.Parse(response.Content)["quoteResponse"]["result"];
                    var countingList = responseObject.Children().ToList().Count;
                    Console.WriteLine(queryString);
                    Console.WriteLine("----------");

                    for (int responseIndex = 0; responseIndex < countingList; responseIndex++)
                    {
                        try
                        {
                            var companyIndexSecondary = companyBD.FindIndex(x => (x.YahooTicker ?? "").ToLower() == (HttpUtility.UrlEncode((responseObject[responseIndex]["symbol"]).ToString()).ToLower()));
                            var dailyInfoId = companyBD[companyIndexSecondary].DailyInfoId.Value;
                            var daily = companyDaily.Find(x => x.Id == dailyInfoId);
                            daily.PreviousClose = daily.StockPrice;
                            //daily.EpsTTM = (decimal)(responseObject[responseIndex]["epsTrailingTwelveMonths"] ?? 0);
                            daily.ForwardPe = (decimal)(responseObject[responseIndex]["forwardPE"] ?? (responseObject[responseIndex]["trailingPE"] ?? 0));
                            daily.StockPrice = (decimal)(responseObject[responseIndex]["regularMarketPreviousClose"] ?? 0);
                            daily.UpdatedOn = DateTime.Now;

                            // ADD
                            //regularMarketPreviousClose
                            //epsTrailingTwelveMonths
                            //forwardPE
                            //
                            listUpdatedDailyInfo.Add(daily);
                        }
                        catch (Exception ex)
                        {

                            WriteResult(ex.Message,"result");
                        }

                    }

                    counting = 0;
                    queryString = "";
                    continue;
                }


                counting++;
            }
            await _genericDao.UpdateRangeAsync<DailyInfo>(listUpdatedDailyInfo);

        }


        public void UpdateEps()
        {


            var companyBD = _genericDao.GetAll<Company>();
            var companyDaily = _genericDao.GetAll<DailyInfo>();

            var queryString = "";

            var listUpdatedDailyInfo = new List<DailyInfo>();
            for (int companyIndex = 0; companyIndex < companyBD.Count; companyIndex++)
            {
                var company = companyBD[companyIndex];
                if (company.DailyInfoId == null)
                {
                    continue;
                }

                queryString= "https://public-api.quickfs.net/v1/data/"+ HttpUtility.UrlEncode(company.Ticker) + "/eps_diluted?period=FY&api_key=d4089a95fc589f2d804c241f4f23b9732ff9ab6e";
                var response = _clientClass.GetAll(queryString);
                var responseObject = JObject.Parse(response.Content)["data"];
                if (responseObject.HasValues)
                {
                    Console.WriteLine(company.Name);
                    companyDaily.Find(x => x.Id == company.DailyInfoId).EpsTTM = (decimal)responseObject[0];
                }


            }
            _genericDao.UpdateRange<DailyInfo>(listUpdatedDailyInfo);

        }
        public async Task UpdatePricesQFS()
        {
            var companyJson = File.ReadAllText(@".\listQFSUpdate.json");
            var companyBD = JsonConvert.DeserializeObject<List<Company>>(companyJson);
            var companyDaily = await _genericDao.GetAllAsync<DailyInfo>();
            var counting = 0;
            var list = new List<DailyPricePoco>();


            var listUpdatedDailyInfo = new List<DailyInfo>();
            for (int companyIndex = 0; companyIndex < companyBD.Count; companyIndex++)
            {
                var company = companyBD[companyIndex];

                var query = "QFS(" + company.Ticker + ",price)";

                list.Add(new DailyPricePoco { Price = query , Ticker = company.Ticker});

                if ((counting % 53 == 0 && counting > 0) || companyIndex == (companyBD.Count - 1))
                {
                    var client = new RestClient("https://public-api.quickfs.net/v1/data/batch");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("x-qfs-api-key", "d4089a95fc589f2d804c241f4f23b9732ff9ab6e");
                    request.RequestFormat = DataFormat.Json;
                    request.AddJsonBody(new { data = list  });

                    var response = client.Execute(request);

                    var responseObject = JObject.Parse(response.Content)["data"];
                    var countingList = responseObject.Children().ToList().Count;
                    Console.WriteLine(company +"  id: "+ company.Id );
                    Console.WriteLine("----------");

                    for (int responseIndex = 0; responseIndex < countingList; responseIndex++)
                    {
                        try
                        {
                            var companyIndexSecondary = companyBD.FindIndex(x => x.Ticker == responseObject[responseIndex]["Ticker"].ToString());
                            var dailyInfoId = companyBD[companyIndexSecondary].DailyInfoId.Value;
                            var daily = companyDaily.Find(x => x.Id == dailyInfoId);
                            daily.PreviousClose = daily.StockPrice;
                            Decimal numericNumber;
                            Decimal.TryParse(responseObject[responseIndex]["Price"].ToString(), out numericNumber);
                            daily.StockPrice = numericNumber;
                            daily.UpdatedOn = DateTime.Now;

                            // ADD
                            //regularMarketPreviousClose
                            //epsTrailingTwelveMonths
                            //forwardPE
                            //
                            listUpdatedDailyInfo.Add(daily);
                        }
                        catch (Exception ex)
                        {

                            WriteResult(ex.Message,"qfsPrice");
                        }

                    }

                    list.Clear();
                    counting = 0;
                    continue;
                }


                counting++;
            }
            await _genericDao.UpdateRangeAsync<DailyInfo>(listUpdatedDailyInfo);

        }


        public bool WriteResult(string result,string file)
        {
            try
            {
                using (StreamWriter sr = File.AppendText(@"C:\Users\User01\source\repos\rpinto1\academy-2021-07-kcs\lasagna\Lasagna\" + file +".txt"))
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
