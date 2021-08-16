using KCSit.SalesforceAcademy.Kappify.DataAccess;
using KCSit.SalesforceAcademy.Lasagna.Data;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Raul
{
    class ProgramKeyRatios
    {
        static void Main(string[] args)
        {
            var genericDao = new GenericDAO();

            var files = new string[] { "dataMM", "dataAU", "dataCA", "dataLN", "dataNZ", "dataUS" };
            foreach (var file in files)
            {



                var KeyRatioListFile = File.ReadAllText(@"C:\Users\User01\source\repos\rpinto1\academy-2021-07-kcs\lasagna\Rui\JsonFiles\" + file + ".txt");
                var jsonKeyRatioList = JObject.Parse(KeyRatioListFile);
                var KeyRatioArray = jsonKeyRatioList["data"]["financials"]["annual"];
                var KeyRatioList = KeyRatioArray.Children().ToList();
                Console.WriteLine(KeyRatioList.Count());
                var KeyRatioObject = new List<KeyRatio>();
                foreach (var item in KeyRatioArray)
                {

                    Console.WriteLine(item["roa"]);
                    Console.WriteLine(item["roe"]);
                    Console.WriteLine(item["roic"]);
                    Console.WriteLine(item["roce"]);
                    Console.WriteLine(item["rotce"]);
                    Console.WriteLine(item["gross_margin"]);
                    Console.WriteLine(item["ebitda_margin"]);
                    Console.WriteLine(item["operating_margin"]);
                    Console.WriteLine(item["pretax_margin"]);
                    Console.WriteLine(item["net_income_margin"]);
                    Console.WriteLine(item["fcf_margin"]);
                    Console.WriteLine(item["assets_to_equity"]);
                    Console.WriteLine(item["equity_to_assets"]);
                    Console.WriteLine(item["debt_to_equity"]);
                    Console.WriteLine(item["debt_to_assets"]);
                    Console.WriteLine(item["revenue_growth"]);
                    Console.WriteLine(item["gross_profit_growth"]);
                    Console.WriteLine(item["ebitda_growth"]);
                    Console.WriteLine(item["operating_income_growth"]);
                    Console.WriteLine(item["pretax_income_growth"]);
                    Console.WriteLine(item["net_income_growth"]);
                    Console.WriteLine(item["eps_diluted_growth"]);
                    Console.WriteLine(item["shares_diluted_growth"]);
                    Console.WriteLine(item["ppe_growth"]);
                    Console.WriteLine(item["total_assets_growth"]);
                    Console.WriteLine(item["total_equity_growth"]);
                    Console.WriteLine(item["cfo_growth"]);
                    Console.WriteLine(item["capex_growth"]);
                    Console.WriteLine(item["fcf_growth"]);
                    Console.WriteLine(item["fcf"]);
                    Console.WriteLine(item["book_value"]);
                    Console.WriteLine(item["tangible_book_value"]);
                    Console.WriteLine(item["revenue_per_share"]);
                    Console.WriteLine(item["ebitda_per_share"]);
                    Console.WriteLine(item["operating_income_per_share"]);
                    Console.WriteLine(item["fcf_per_share"]);
                    Console.WriteLine(item["book_value_per_share"]);
                    Console.WriteLine(item["tangible_book_per_share"]);
                    Console.WriteLine(item["market_cap"]);
                    Console.WriteLine(item["price_to_earnings"]);
                    Console.WriteLine(item["price_to_book"]);
                    Console.WriteLine(item["price_to_sales"]);
                    Console.WriteLine(item["dividends"]);
                    Console.WriteLine(item["payout_ratio"]);

                    KeyRatioObject.Add(new KeyRatio
                    {
                        ReturnOnAssets = System.Convert.ToDecimal(item["roa"].ToString()),
                        ReturnOnEquity = System.Convert.ToDecimal(item["roe"].ToString()),
                        ReturnOnInvestedCapital = System.Convert.ToDecimal(item["roic"].ToString()),
                        ReturnOnCapitalEmployed = System.Convert.ToDecimal(item["roce"].ToString()),
                        //ReturnOnTangibleCapitalEmployed = System.Convert.ToDecimal(item["rotce"].ToString()),
                        GrossMargin = System.Convert.ToDecimal(item["gross_margin"].ToString()),
                        Ebidtamargin = System.Convert.ToDecimal(item["ebitda_margin"].ToString()),
                        OperatingMargin = System.Convert.ToDecimal(item["operating_margin"].ToString()),
                        PretaxMargin = System.Convert.ToDecimal(item["pretax_margin"].ToString()),
                        NetMargin = System.Convert.ToDecimal(item["net_income_margin"].ToString()),
                        FreeCashMargin = System.Convert.ToDecimal(item["fcf_margin"].ToString()),
                        AssetsToEquity = System.Convert.ToDecimal(item["assets_to_equity"].ToString()),
                        EquityToAssets = System.Convert.ToDecimal(item["equity_to_assets"].ToString()),
                        DebtToEquity = System.Convert.ToDecimal(item["debt_to_equity"].ToString()),
                        DebtToAssets = System.Convert.ToDecimal(item["debt_to_assets"].ToString()),
                        RevenueGrowth = System.Convert.ToDecimal(item["revenue_growth"].ToString()),
                        GrossProfitGrowth = System.Convert.ToDecimal(item["gross_profit_growth"].ToString()),
                        Ebidtagrowth = System.Convert.ToDecimal(item["ebitda_growth"].ToString()),
                        OperatingIncomeGrowth = System.Convert.ToDecimal(item["operating_income_growth"].ToString()),
                        PretaxIncomeGrowth = System.Convert.ToDecimal(item["pretax_income_growth"].ToString()),
                        NetIncomeGrowth = System.Convert.ToDecimal(item["net_income_growth"].ToString()),
                        DilutedEpsgrowth = System.Convert.ToDecimal(item["eps_diluted_growth"].ToString()),
                        DilutedSharesGrowth = System.Convert.ToDecimal(item["shares_diluted_growth"].ToString()),
                        Ppegrowth = System.Convert.ToDecimal(item["ppe_growth"].ToString()),
                        TotalAssetsGrowth = System.Convert.ToDecimal(item["total_assets_growth"].ToString()),
                        EquityGrowth = System.Convert.ToDecimal(item["total_equity_growth"].ToString()),
                        CashFromOperationsGrowth = System.Convert.ToDecimal(item["cfo_growth"].ToString()),
                        CapitalExpendituresGrowth = System.Convert.ToDecimal(item["capex_growth"].ToString()),
                        FreeCashFlowGrowth = System.Convert.ToDecimal(item["fcf_growth"].ToString()),
                        FreeCashFlow = System.Convert.ToDecimal(item["fcf"].ToString()),
                        BookValue = System.Convert.ToDecimal(item["book_value"].ToString()),
                        TangibleBookValue = System.Convert.ToDecimal(item["tangible_book_value"].ToString()),
                        RevenuePerShare = System.Convert.ToDecimal(item["revenue_per_share"].ToString()),
                        EbidtaperShare = System.Convert.ToDecimal(item["ebitda_per_share"].ToString()),
                        OperatingIncomePerShare = System.Convert.ToDecimal(item["operating_income_per_share"].ToString()),
                        FreeCashFlowPerShare = System.Convert.ToDecimal(item["fcf_per_share"].ToString()),
                        BookValuePerShare = System.Convert.ToDecimal(item["book_value_per_share"].ToString()),
                        TangibleBookValuePerShare = System.Convert.ToDecimal(item["tangible_book_per_share"].ToString()),
                        MarketCapitalization = System.Convert.ToDecimal(item["market_cap"].ToString()),
                        PriceToEarnings = System.Convert.ToDecimal(item["price_to_earnings"].ToString()),
                        PriceToBook = System.Convert.ToDecimal(item["price_to_book"].ToString()),
                        PriceToSales = System.Convert.ToDecimal(item["price_to_sales"].ToString()),
                        DividendsPerShare = System.Convert.ToDecimal(item["dividends"].ToString()),
                        PayoutRatio = System.Convert.ToDecimal(item["payout_ratio"].ToString()),


                        Uuid = Guid.NewGuid()
                    });

                }

                genericDao.AddRange<KeyRatio>(KeyRatioObject);
            }

            Console.WriteLine("Enter user api Key");
            string apiKey = Console.ReadLine();

            var clientClass = new Client();

            IRestResponse responseList = clientClass.GetAll("https:public-api.quickfs.net/v1/companies?api_key=" + apiKey);

            var responseKeyRatioList = JObject.Parse(responseList.Content)["data"];


            for (int i = 1; i < responseKeyRatioList.ToObject<string[]>().Length; i++)
            {


                if (clientClass.CheckQuota(apiKey) < 2000)
                {

                    Environment.Exit(0);
                }

                IRestResponse response = clientClass.GetAll("https:public-api.quickfs.net/v1/data/all-data/" + responseKeyRatioList[i].ToString() + "?api_key=" + apiKey);

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
