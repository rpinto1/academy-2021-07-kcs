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

namespace Rui.tables.bank
{
    class BalanceSheetsBank
    {
        GenericDAO genericDao;
        public BalanceSheetsBank(GenericDAO genericDaoOut)
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
                TotalInvestments = System.Convert.ToDecimal(item["total_investments"][index].ToString()),
                GrossLoans = System.Convert.ToDecimal(item["loans_gross"][index].ToString()),
                AllowanceLoanLosses = System.Convert.ToDecimal(item["allowance_for_loan_losses"][index].ToString()),
                UnearnedIncome = System.Convert.ToDecimal(item["unearned_income"][index].ToString()),
                NetLoans = System.Convert.ToDecimal(item["loans_net"][index].ToString()),
                PropertyPlantAndEquipment = System.Convert.ToDecimal(item["ppe_net"][index].ToString()),
                Goodwill = System.Convert.ToDecimal(item["goodwill"][index].ToString()),
                OtherIntangibleAssets = System.Convert.ToDecimal(item["intangible_assets"][index].ToString()),
                 OtherAssets = System.Convert.ToDecimal(item["other_lt_assets"][index].ToString()),
                TotalAssets = System.Convert.ToDecimal(item["total_assets"][index].ToString()),
                DepositsLiability = System.Convert.ToDecimal(item["deposits_liability"][index].ToString()),
                ShortTermDebt = System.Convert.ToDecimal(item["st_debt"][index].ToString()),
                LongTermDebt = System.Convert.ToDecimal(item["lt_debt"][index].ToString()),
                OtherLiabilities = System.Convert.ToDecimal(item["other_lt_liabilities"][index].ToString()),
                TotalLiabilities = System.Convert.ToDecimal(item["total_liabilities"][index].ToString()),
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