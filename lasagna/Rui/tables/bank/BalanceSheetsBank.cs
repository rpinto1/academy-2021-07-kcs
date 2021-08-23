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
using System.Threading.Tasks;
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
        public Task<BalanceSheet> insertBalanceSheets(String balanceSheets, int index)
        {

            var jsonCompanyList = JObject.Parse(balanceSheets);
            var companyArray = jsonCompanyList["data"];

            //criar keyStatistic
            var item = companyArray["financials"]["annual"];


            var balance = new BalanceSheet
            {

                CashAndEquivalents = Decimal.Parse(item["cash_and_equiv"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                TotalInvestments = Decimal.Parse(item["total_investments"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                GrossLoans = Decimal.Parse(item["loans_gross"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                AllowanceLoanLosses = Decimal.Parse(item["allowance_for_loan_losses"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                UnearnedIncome = Decimal.Parse(jsonCompanyList["data"]["financials"]["annual"]["unearned_income"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                NetLoans = Decimal.Parse(item["loans_net"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                PropertyPlantAndEquipment = Decimal.Parse(item["ppe_net"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                Goodwill = Decimal.Parse(item["goodwill"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                OtherIntangibleAssets = Decimal.Parse(item["intangible_assets"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                 OtherAssets = Decimal.Parse(item["other_lt_assets"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                TotalAssets = Decimal.Parse(item["total_assets"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                DepositsLiability = Decimal.Parse(item["deposits_liability"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                ShortTermDebt = Decimal.Parse(item["st_debt"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                LongTermDebt = Decimal.Parse(item["lt_debt"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
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