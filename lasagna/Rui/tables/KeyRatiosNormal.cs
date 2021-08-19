﻿using KCSit.SalesforceAcademy.Kappify.DataAccess;
using KCSit.SalesforceAcademy.Lasagna.Data;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rui.tables
{
    class KeyRatiosNormal
    {
        GenericDAO genericDao;
        public KeyRatiosNormal(GenericDAO genericDaoOut)
        {
            genericDao = genericDaoOut;
        }
        public int InsertKeyRatios(String keyRatios, int index)
        {


            var jsonCompanyList = JObject.Parse(keyRatios);
            var companyArray = jsonCompanyList["data"];

            //criar keyStatistic
            var item = companyArray["financials"]["annual"];

            //Console.WriteLine(item["roa"]);
            //        Console.WriteLine(item["roe"]);
            //        Console.WriteLine(item["roic"]);
            //        Console.WriteLine(item["roce"]);
            //        Console.WriteLine(item["rotce"]);
            //        Console.WriteLine(item["gross_margin"]);
            //        Console.WriteLine(item["ebitda_margin"]);
            //        Console.WriteLine(item["operating_margin"]);
            //        Console.WriteLine(item["pretax_margin"]);
            //        Console.WriteLine(item["net_income_margin"]);
            //        Console.WriteLine(item["fcf_margin"]);
            //        Console.WriteLine(item["assets_to_equity"]);
            //        Console.WriteLine(item["equity_to_assets"]);
            //        Console.WriteLine(item["debt_to_equity"]);
            //        Console.WriteLine(item["debt_to_assets"]);
            //        Console.WriteLine(item["revenue_growth"]);
            //        Console.WriteLine(item["gross_profit_growth"]);
            //        Console.WriteLine(item["ebitda_growth"]);
            //        Console.WriteLine(item["operating_income_growth"]);
            //        Console.WriteLine(item["pretax_income_growth"]);
            //        Console.WriteLine(item["net_income_growth"]);
            //        Console.WriteLine(item["eps_diluted_growth"]);
            //        Console.WriteLine(item["shares_diluted_growth"]);
            //        Console.WriteLine(item["ppe_growth"]);
            //        Console.WriteLine(item["total_assets_growth"]);
            //        Console.WriteLine(item["total_equity_growth"]);
            //        Console.WriteLine(item["cfo_growth"]);
            //        Console.WriteLine(item["capex_growth"]);
            //        Console.WriteLine(item["fcf_growth"]);
            //        Console.WriteLine(item["fcf"]);
            //        Console.WriteLine(item["book_value"]);
            //        Console.WriteLine(item["tangible_book_value"]);
            //        Console.WriteLine(item["revenue_per_share"]);
            //        Console.WriteLine(item["ebitda_per_share"]);
            //        Console.WriteLine(item["operating_income_per_share"]);
            //        Console.WriteLine(item["fcf_per_share"]);
            //        Console.WriteLine(item["book_value_per_share"]);
            //        Console.WriteLine(item["tangible_book_per_share"]);
            //        Console.WriteLine(item["market_cap"]);
            //        Console.WriteLine(item["price_to_earnings"]);
            //        Console.WriteLine(item["price_to_book"]);
            //        Console.WriteLine(item["price_to_sales"]);
            //        Console.WriteLine(item["dividends"]);
            //        Console.WriteLine(item["payout_ratio"]);

                    var KeyRatioObject =  new KeyRatio
                    {
                        ReturnOnAssets = System.Convert.ToDecimal(item["roa"][index].ToString()),
                        ReturnOnEquity = System.Convert.ToDecimal(item["roe"][index].ToString()),
                        ReturnOnInvestedCapital = System.Convert.ToDecimal(item["roic"][index].ToString()),
                        ReturnOnCapitalEmployed = System.Convert.ToDecimal(item["roce"][index].ToString()),
                        ReturnOnTangibleCapitalEmployed = System.Convert.ToDecimal(item["rotce"][index].ToString()),
                        GrossMargin = System.Convert.ToDecimal(item["gross_margin"][index].ToString()),
                        Ebidtamargin = System.Convert.ToDecimal(item["ebitda_margin"][index].ToString()),
                        OperatingMargin = System.Convert.ToDecimal(item["operating_margin"][index].ToString()),
                        PretaxMargin = System.Convert.ToDecimal(item["pretax_margin"][index].ToString()),
                        NetMargin = System.Convert.ToDecimal(item["net_income_margin"][index].ToString()),
                        FreeCashMargin = System.Convert.ToDecimal(item["fcf_margin"][index].ToString()),
                        AssetsToEquity = System.Convert.ToDecimal(item["assets_to_equity"][index].ToString()),
                        EquityToAssets = System.Convert.ToDecimal(item["equity_to_assets"][index].ToString()),
                        DebtToEquity = System.Convert.ToDecimal(item["debt_to_equity"][index].ToString()),
                        DebtToAssets = System.Convert.ToDecimal(item["debt_to_assets"][index].ToString()),
                        RevenueGrowth = System.Convert.ToDecimal(item["revenue_growth"][index].ToString()),
                        GrossProfitGrowth = System.Convert.ToDecimal(item["gross_profit_growth"][index].ToString()),
                        Ebidtagrowth = System.Convert.ToDecimal(item["ebitda_growth"][index].ToString()),
                        OperatingIncomeGrowth = System.Convert.ToDecimal(item["operating_income_growth"][index].ToString()),
                        PretaxIncomeGrowth = System.Convert.ToDecimal(item["pretax_income_growth"][index].ToString()),
                        NetIncomeGrowth = System.Convert.ToDecimal(item["net_income_growth"][index].ToString()),
                        DilutedEpsgrowth = System.Convert.ToDecimal(item["eps_diluted_growth"][index].ToString()),
                        DilutedSharesGrowth = System.Convert.ToDecimal(item["shares_diluted_growth"][index].ToString()),
                        Ppegrowth = System.Convert.ToDecimal(item["ppe_growth"][index].ToString()),
                        TotalAssetsGrowth = System.Convert.ToDecimal(item["total_assets_growth"][index].ToString()),
                        EquityGrowth = System.Convert.ToDecimal(item["total_equity_growth"][index].ToString()),
                        CashFromOperationsGrowth = System.Convert.ToDecimal(item["cfo_growth"][index].ToString()),
                        CapitalExpendituresGrowth = System.Convert.ToDecimal(item["capex_growth"][index].ToString()),
                        FreeCashFlowGrowth = System.Convert.ToDecimal(item["fcf_growth"][index].ToString()),
                        FreeCashFlow = System.Convert.ToDecimal(item["fcf"][index].ToString()),
                        BookValue = System.Convert.ToDecimal(item["book_value"][index].ToString()),
                        TangibleBookValue = System.Convert.ToDecimal(item["tangible_book_value"][index].ToString()),
                        RevenuePerShare = System.Convert.ToDecimal(item["revenue_per_share"][index].ToString()),
                        EbidtaperShare = System.Convert.ToDecimal(item["ebitda_per_share"][index].ToString()),
                        OperatingIncomePerShare = System.Convert.ToDecimal(item["operating_income_per_share"][index].ToString()),
                        FreeCashFlowPerShare = System.Convert.ToDecimal(item["fcf_per_share"][index].ToString()),
                        BookValuePerShare = System.Convert.ToDecimal(item["book_value_per_share"][index].ToString()),
                        TangibleBookValuePerShare = System.Convert.ToDecimal(item["tangible_book_per_share"][index].ToString()),
                        MarketCapitalization = System.Convert.ToDecimal(item["market_cap"][index].ToString()),
                        PriceToEarnings = System.Convert.ToDecimal(item["price_to_earnings"][index].ToString()),
                        PriceToBook = System.Convert.ToDecimal(item["price_to_book"][index].ToString()),
                        PriceToSales = System.Convert.ToDecimal(item["price_to_sales"][index].ToString()),
                        DividendsPerShare = System.Convert.ToDecimal(item["dividends"][index].ToString()),
                        PayoutRatio = System.Convert.ToDecimal(item["payout_ratio"][index].ToString()),
                        Uuid = Guid.NewGuid()
                    };





            //var keyRatio = genericDao.Add<KeyRatio>(KeyRatioObject);

            return 1; //keyRatio.Id;
        }
    }
}