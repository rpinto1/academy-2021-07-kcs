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

namespace Rui.tables.insurance
{
    class BalanceSheetsInsurance
    {
        GenericDAO genericDao;
        public BalanceSheetsInsurance(GenericDAO genericDaoOut)
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

                
                AccountsReceivable = System.Convert.ToDecimal(item["receivables"][index].ToString()),
                Investments = System.Convert.ToDecimal(item["total_investments"][index].ToString()),
                PropertyPlantAndEquipment = System.Convert.ToDecimal(item["ppe_net"][index].ToString()),
                OtherIntangibleAssets = System.Convert.ToDecimal(item["intangible_assets"][index].ToString()),
                OtherAssets = System.Convert.ToDecimal(item["other_lt_assets"][index].ToString()),   
                TotalAssets = System.Convert.ToDecimal(item["total_assets"][index].ToString()),
                ShortTermDebt = System.Convert.ToDecimal(item["st_debt"][index].ToString()),
                LongTermDebt = System.Convert.ToDecimal(item["lt_debt"][index].ToString()),
                OtherLiabilities = System.Convert.ToDecimal(item["other_lt_liabilities"][index].ToString()),
                TotalLiabilities = System.Convert.ToDecimal(item["total_liabilities"][index].ToString()),
                OtherCurrentLabilities = System.Convert.ToDecimal(item["other_current_liabilities"][index].ToString()),
                FuturePolicyBenefits = System.Convert.ToDecimal(item["future_policy_benefits"][index].ToString()),
                DeferredPolicyCost = System.Convert.ToDecimal(item["deferred_policy_acquisition_cost"][index].ToString()),
                RetainedEarnings = System.Convert.ToDecimal(item["retained_earnings"][index].ToString()),
                CommonStock = System.Convert.ToDecimal(item["common_stock"][index].ToString()),
                Aoci = System.Convert.ToDecimal(item["aoci"][index].ToString()),
                ShareholdersEquity = System.Convert.ToDecimal(item["total_equity"][index].ToString()),
                TotalLiabilitiesAndEquity = System.Convert.ToDecimal(item["total_liabilities_and_equity"][index].ToString()),
                Uuid = Guid.NewGuid()
            };

            var balanceAdded = genericDao.Add<BalanceSheet>(balance);

            return balanceAdded.Id;

        }
    }
}