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
using System.Threading.Tasks;
using System.Linq;

namespace Rui.tables.insurance
{
    class BalanceSheetsInsurance
    {
        GenericDAO genericDao;
        public BalanceSheetsInsurance(GenericDAO genericDaoOut)
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

                
                AccountsReceivable = Decimal.Parse(item["receivables"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                Investments = Decimal.Parse(item["total_investments"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                PropertyPlantAndEquipment = Decimal.Parse(item["ppe_net"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                OtherIntangibleAssets = Decimal.Parse(item["intangible_assets"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                OtherAssets = Decimal.Parse(item["other_lt_assets"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),   
                TotalAssets = Decimal.Parse(item["total_assets"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                ShortTermDebt = Decimal.Parse(item["st_debt"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                LongTermDebt = Decimal.Parse(item["lt_debt"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                OtherLiabilities = Decimal.Parse(item["other_lt_liabilities"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                TotalLiabilities = Decimal.Parse(item["total_liabilities"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                OtherCurrentLabilities = Decimal.Parse(item["other_current_liabilities"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                FuturePolicyBenefits = Decimal.Parse(item["future_policy_benefits"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                DeferredPolicyCost = Decimal.Parse(item["deferred_policy_acquisition_cost"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
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