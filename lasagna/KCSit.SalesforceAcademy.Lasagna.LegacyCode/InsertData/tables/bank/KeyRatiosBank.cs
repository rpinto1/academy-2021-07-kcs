using KCSit.SalesforceAcademy.Lasagna.DataAccess;
using KCSit.SalesforceAcademy.Lasagna.Data;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace Rui.tables.bank
{
    class KeyRatiosBank
    {
        GenericDAO genericDao;
        public KeyRatiosBank(GenericDAO genericDaoOut)
        {
            genericDao = genericDaoOut;
        }
        public Task<KeyRatio> InsertKeyRatios(String keyRatios, int index)
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
                        ReturnOnAssets = Decimal.Parse(item["roa"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        ReturnOnEquity = Decimal.Parse(item["roe"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        ReturnOnInvestedCapital = Decimal.Parse(item["roic"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        ReturnOnCapitalEmployed = Decimal.Parse(item["roce"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        NetMargin = Decimal.Parse(item["net_income_margin"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        AssetsToEquity = Decimal.Parse(item["assets_to_equity"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        EquityToAssets = Decimal.Parse(item["equity_to_assets"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        EarningAssetsToEquity = Decimal.Parse(item["earning_assets_to_equity"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        DebtToEquity = Decimal.Parse(item["debt_to_equity"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        RevenuePerShare = Decimal.Parse(item["revenue_per_share"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        EbidtaperShare = Decimal.Parse(item["ebitda_per_share"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),                       
                        BookValuePerShare = Decimal.Parse(item["book_value_per_share"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        TangibleBookValuePerShare = Decimal.Parse(item["tangible_book_per_share"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        RevenueGrowth = Decimal.Parse(item["revenue_growth"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        Ebidtagrowth = Decimal.Parse(item["ebitda_growth"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        NetInterestIncomeGrowthBank = Decimal.Parse(item["net_interest_income_growth"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        NetIncomeGrowth = Decimal.Parse(item["net_income_growth"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        DilutedEpsgrowth = Decimal.Parse(item["eps_diluted_growth"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        DilutedSharesGrowth = Decimal.Parse(item["shares_diluted_growth"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        CashFromOperationsGrowth = Decimal.Parse(item["cash_and_equiv_growth"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        GrossLoansGrowth = Decimal.Parse(item["loans_gross_growth"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        EarningAssetsGrowth = Decimal.Parse(item["earning_assets_growth"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        TotalAssetsGrowth = Decimal.Parse(item["total_assets_growth"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        EquityGrowth = Decimal.Parse(item["total_equity_growth"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        TotalDepositGrowth = Decimal.Parse(item["deposits_growth"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        MarketCapitalization = Decimal.Parse(item["market_cap"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        PriceToEarnings = Decimal.Parse(item["price_to_earnings"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        PriceToBook = Decimal.Parse(item["price_to_book"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        PriceToSales = Decimal.Parse(item["price_to_sales"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        DividendsPerShare = Decimal.Parse(item["dividends"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        PayoutRatio = Decimal.Parse(item["payout_ratio"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        Uuid = Guid.NewGuid()
                    };





           return genericDao.AddAsync<KeyRatio>(KeyRatioObject);

        }
    }
}
