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

namespace Rui.tables
{
    class BalanceSheetsNormal

    {
        GenericDAO genericDao;
        public BalanceSheetsNormal(GenericDAO genericDaoOut)
        {
            genericDao = genericDaoOut;
        }
        public int insertBalanceSheets(String balanceSheets, int index)
        {

            var jsonCompanyList = JObject.Parse(balanceSheets);
            var companyArray = jsonCompanyList["data"];

            //criar keyStatistic
            var item = companyArray["financials"]["annual"];


            var balance = new BalanceSheet
            {

                CashAndEquivalents = System.Convert.ToDecimal(item["cash_and_equiv"][index].ToString()),
                ShortTermInvestments = System.Convert.ToDecimal(item["st_investments"][index].ToString()),
                AccountsReceivable = System.Convert.ToDecimal(item["receivables"][index].ToString()),
                Inventories = System.Convert.ToDecimal(item["inventories"][index].ToString()),
                OtherCurrentAssets = System.Convert.ToDecimal(item["other_current_assets"][index].ToString()),
                TotalCurrentAssets = System.Convert.ToDecimal(item["total_current_assets"][index].ToString()),
                Investments = System.Convert.ToDecimal(item["equity_and_other_investments"][index].ToString()),
                PropertyPlantAndEquipment = System.Convert.ToDecimal(item["ppe_net"][index].ToString()),
                Goodwill = System.Convert.ToDecimal(item["goodwill"][index].ToString()),
                OtherIntangibleAssets = System.Convert.ToDecimal(item["intangible_assets"][index].ToString()),
                OtherAssets = System.Convert.ToDecimal(item["other_lt_assets"][index].ToString()),
                TotalAssets = System.Convert.ToDecimal(item["total_assets"][index].ToString()),
                AccountsPayable = System.Convert.ToDecimal(item["accounts_payable"][index].ToString()),
                TaxPayable = System.Convert.ToDecimal(item["tax_payable"][index].ToString()),
                ShortTermDebt = System.Convert.ToDecimal(item["st_debt"][index].ToString()),
                CurrentDeferredRevenue = System.Convert.ToDecimal(item["current_deferred_revenue"][index].ToString()),
                OtherCurrentLabilities = System.Convert.ToDecimal(item["other_current_liabilities"][index].ToString()),
                TotalCurrentLiabilities = System.Convert.ToDecimal(item["total_current_liabilities"][index].ToString()),
                LongTermDebt = System.Convert.ToDecimal(item["lt_debt"][index].ToString()),
                CapitalLeases = System.Convert.ToDecimal(item["noncurrent_capital_leases"][index].ToString()),
                NonCurrentDeferredRevenue = System.Convert.ToDecimal(item["noncurrent_deferred_revenue"][index].ToString()),
                OtherLiabilities = System.Convert.ToDecimal(item["other_lt_liabilities"][index].ToString()),
                TotalLiabilities = System.Convert.ToDecimal(item["total_liabilities"][index].ToString()),
                RetainedEarnings = System.Convert.ToDecimal(item["retained_earnings"][index].ToString()),
                CommonStock = System.Convert.ToDecimal(item["common_stock"][index].ToString()),
                Aoci = System.Convert.ToDecimal(item["aoci"][index].ToString()),
                ShareholdersEquity = System.Convert.ToDecimal(item["total_equity"][index].ToString()),
                TotalLiabilitiesAndEquity = System.Convert.ToDecimal(item["total_liabilities_and_equity"][index].ToString()),
                Uuid = Guid.NewGuid()
            };

            //var balanceAdded = genericDao.Add<BalanceSheet>(balance);

            return 1; //balanceAdded.Id;

        }
    }
}