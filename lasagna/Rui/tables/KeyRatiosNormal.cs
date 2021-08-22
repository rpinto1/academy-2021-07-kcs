using KCSit.SalesforceAcademy.Kappify.DataAccess;
using KCSit.SalesforceAcademy.Lasagna.Data;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rui.tables
{
    class KeyRatiosNormal
    {
        GenericDAO genericDao;
        public KeyRatiosNormal(GenericDAO genericDaoOut)
        {
            genericDao = genericDaoOut;
        }
        public Task<KeyRatio> InsertKeyRatios(String keyRatios, int index)
        {


            var jsonCompanyList = JObject.Parse(keyRatios);
            var companyArray = jsonCompanyList["data"];

            //criar keyStatistic
            var item = companyArray["financials"]["annual"];

            var KeyRatioObject =  new KeyRatio
                    {
                ReturnOnAssets = Decimal.Parse(item["roa"][index].ToString(), System.Globalization.NumberStyles.Float),
                ReturnOnEquity = Decimal.Parse(item["roe"][index].ToString(), System.Globalization.NumberStyles.Float),
                ReturnOnInvestedCapital = Decimal.Parse(item["roic"][index].ToString(), System.Globalization.NumberStyles.Float),
                ReturnOnCapitalEmployed = Decimal.Parse(item["roce"][index].ToString(), System.Globalization.NumberStyles.Float),
                ReturnOnTangibleCapitalEmployed = Decimal.Parse(item["rotce"][index].ToString(), System.Globalization.NumberStyles.Float),
                GrossMargin = Decimal.Parse(item["gross_margin"][index].ToString(), System.Globalization.NumberStyles.Float),
                Ebidtamargin = Decimal.Parse(item["ebitda_margin"][index].ToString(), System.Globalization.NumberStyles.Float),
                OperatingMargin = Decimal.Parse(item["operating_margin"][index].ToString(), System.Globalization.NumberStyles.Float),
                PretaxMargin = Decimal.Parse(item["pretax_margin"][index].ToString(), System.Globalization.NumberStyles.Float),
                NetMargin = Decimal.Parse(item["net_income_margin"][index].ToString(), System.Globalization.NumberStyles.Float),
                FreeCashMargin = Decimal.Parse(item["fcf_margin"][index].ToString(), System.Globalization.NumberStyles.Float),
                AssetsToEquity = Decimal.Parse(item["assets_to_equity"][index].ToString(), System.Globalization.NumberStyles.Float),
                EquityToAssets = Decimal.Parse(item["equity_to_assets"][index].ToString(), System.Globalization.NumberStyles.Float),
                DebtToEquity = Decimal.Parse(item["debt_to_equity"][index].ToString(), System.Globalization.NumberStyles.Float),
                DebtToAssets = Decimal.Parse(item["debt_to_assets"][index].ToString(), System.Globalization.NumberStyles.Float),
                RevenueGrowth = Decimal.Parse(item["revenue_growth"][index].ToString(), System.Globalization.NumberStyles.Float),
                GrossProfitGrowth = Decimal.Parse(item["gross_profit_growth"][index].ToString(), System.Globalization.NumberStyles.Float),
                Ebidtagrowth = Decimal.Parse(item["ebitda_growth"][index].ToString(), System.Globalization.NumberStyles.Float),
                OperatingIncomeGrowth = Decimal.Parse(item["operating_income_growth"][index].ToString(), System.Globalization.NumberStyles.Float),
                PretaxIncomeGrowth = Decimal.Parse(item["pretax_income_growth"][index].ToString(), System.Globalization.NumberStyles.Float),
                NetIncomeGrowth = Decimal.Parse(item["net_income_growth"][index].ToString(), System.Globalization.NumberStyles.Float),
                DilutedEpsgrowth = Decimal.Parse(item["eps_diluted_growth"][index].ToString(), System.Globalization.NumberStyles.Float),
                DilutedSharesGrowth = Decimal.Parse(item["shares_diluted_growth"][index].ToString(), System.Globalization.NumberStyles.Float),
                Ppegrowth = Decimal.Parse(item["ppe_growth"][index].ToString(), System.Globalization.NumberStyles.Float),
                TotalAssetsGrowth = Decimal.Parse(item["total_assets_growth"][index].ToString(), System.Globalization.NumberStyles.Float),
                EquityGrowth = Decimal.Parse(item["total_equity_growth"][index].ToString(), System.Globalization.NumberStyles.Float),
                CashFromOperationsGrowth = Decimal.Parse(item["cfo_growth"][index].ToString(), System.Globalization.NumberStyles.Float),
                CapitalExpendituresGrowth = Decimal.Parse(item["capex_growth"][index].ToString(), System.Globalization.NumberStyles.Float),
                FreeCashFlowGrowth = Decimal.Parse(item["fcf_growth"][index].ToString(), System.Globalization.NumberStyles.Float),
                FreeCashFlow = Decimal.Parse(item["fcf"][index].ToString(), System.Globalization.NumberStyles.Float),
                BookValue = Decimal.Parse(item["book_value"][index].ToString(), System.Globalization.NumberStyles.Float),
                TangibleBookValue = Decimal.Parse(item["tangible_book_value"][index].ToString(), System.Globalization.NumberStyles.Float),
                RevenuePerShare = Decimal.Parse(item["revenue_per_share"][index].ToString(), System.Globalization.NumberStyles.Float),
                EbidtaperShare = Decimal.Parse(item["ebitda_per_share"][index].ToString(), System.Globalization.NumberStyles.Float),
                OperatingIncomePerShare = Decimal.Parse(item["operating_income_per_share"][index].ToString(), System.Globalization.NumberStyles.Float),
                FreeCashFlowPerShare = Decimal.Parse(item["fcf_per_share"][index].ToString(), System.Globalization.NumberStyles.Float),
                BookValuePerShare = Decimal.Parse(item["book_value_per_share"][index].ToString(), System.Globalization.NumberStyles.Float),
                TangibleBookValuePerShare = Decimal.Parse(item["tangible_book_per_share"][index].ToString(), System.Globalization.NumberStyles.Float),
                MarketCapitalization = Decimal.Parse(item["market_cap"][index].ToString(), System.Globalization.NumberStyles.Float),
                PriceToEarnings = Decimal.Parse(item["price_to_earnings"][index].ToString(), System.Globalization.NumberStyles.Float),
                PriceToBook = Decimal.Parse(item["price_to_book"][index].ToString(), System.Globalization.NumberStyles.Float),
                PriceToSales = Decimal.Parse(item["price_to_sales"][index].ToString(), System.Globalization.NumberStyles.Float),
                DividendsPerShare = Decimal.Parse(item["dividends"][index].ToString(), System.Globalization.NumberStyles.Float),
                PayoutRatio = Decimal.Parse(item["payout_ratio"][index].ToString(), System.Globalization.NumberStyles.Float),
                Uuid = Guid.NewGuid()
                    };





            var keyRatio = genericDao.AddAsync<KeyRatio>(KeyRatioObject);

            return keyRatio;
        }
    }
}
