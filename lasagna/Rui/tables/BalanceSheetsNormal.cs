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
using System.Threading.Tasks;

namespace Rui.tables
{
    class BalanceSheetsNormal

    {
        GenericDAO genericDao;
        public BalanceSheetsNormal(GenericDAO genericDaoOut)
        {
            genericDao = genericDaoOut;
        }
        public Task<BalanceSheet> insertBalanceSheets(String balanceSheets, int index)
        {

            var jsonCompanyList = JObject.Parse(balanceSheets);
            var companyArray = jsonCompanyList["data"];

            //criar keyStatistic
            var item = companyArray["financials"]["annual"];


            var balance = new BalanceSheet
            {

                CashAndEquivalents = Decimal.Parse(item["cash_and_equiv"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                ShortTermInvestments = Decimal.Parse(item["st_investments"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                AccountsReceivable = Decimal.Parse(item["receivables"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                Inventories = Decimal.Parse(item["inventories"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                OtherCurrentAssets = Decimal.Parse(item["other_current_assets"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                TotalCurrentAssets = Decimal.Parse(item["total_current_assets"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                Investments = Decimal.Parse(item["equity_and_other_investments"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                PropertyPlantAndEquipment = Decimal.Parse(item["ppe_net"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                Goodwill = Decimal.Parse(item["goodwill"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                OtherIntangibleAssets = Decimal.Parse(item["intangible_assets"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                OtherAssets = Decimal.Parse(item["other_lt_assets"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                TotalAssets = Decimal.Parse(item["total_assets"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                AccountsPayable = Decimal.Parse(item["accounts_payable"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                TaxPayable = Decimal.Parse(item["tax_payable"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                ShortTermDebt = Decimal.Parse(item["st_debt"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                CurrentDeferredRevenue = Decimal.Parse(item["current_deferred_revenue"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                OtherCurrentLabilities = Decimal.Parse(item["other_current_liabilities"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                TotalCurrentLiabilities = Decimal.Parse(item["total_current_liabilities"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                LongTermDebt = Decimal.Parse(item["lt_debt"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                CapitalLeases = Decimal.Parse(item["noncurrent_capital_leases"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                NonCurrentDeferredRevenue = Decimal.Parse(item["noncurrent_deferred_revenue"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                OtherLiabilities = Decimal.Parse(item["other_lt_liabilities"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                TotalLiabilities = Decimal.Parse(item["total_liabilities"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                RetainedEarnings = Decimal.Parse(item["retained_earnings"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                CommonStock = Decimal.Parse(item["common_stock"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                Aoci = Decimal.Parse(item["aoci"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                ShareholdersEquity = Decimal.Parse(item["total_equity"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                TotalLiabilitiesAndEquity = Decimal.Parse(item["total_liabilities_and_equity"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                Uuid = Guid.NewGuid()
            };

            


            return genericDao.AddAsync<BalanceSheet>(balance);

        }
    }
}