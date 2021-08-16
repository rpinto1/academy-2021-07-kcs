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
    class BalanceSheets
    {
        GenericDAO genericDao;
        public BalanceSheets(GenericDAO genericDaoOut)
        {
            genericDao = genericDaoOut;
        }
        public int insertIncomeStatements(String balanceSheets, int index)
        {

            var jsonCompanyList = JObject.Parse(balanceSheets);
            var companyArray = jsonCompanyList["data"];

            //criar keyStatistic
            var item = companyArray["financials"]["annual"];
            var CompanyObject = new List<BalanceSheet>();

            var balance = new BalanceSheet
            {

                CashAndEquivalents = System.Convert.ToDecimal(item["revenue"][index].ToString()),
                ShortTermInvestments = System.Convert.ToDecimal(item["cogs"][index].ToString()),
                AccountsReceivable = System.Convert.ToDecimal(item["gross_profit"][index].ToString()),
                Inventories = System.Convert.ToDecimal(item["sga"][index].ToString()),
                OtherCurrentAssets = System.Convert.ToDecimal(item["rnd"][index].ToString()),
                TotalCurrentAssets = System.Convert.ToDecimal(item["other_opex"].ToString()),
                Investments = System.Convert.ToDecimal(item["total_opex"].ToString()),
                PropertyPlantAndEquipment = System.Convert.ToDecimal(item["operating_income"].ToString()),
                Goodwill = System.Convert.ToDecimal(item["interest_income"].ToString()),
                OtherIntangibleAssets = System.Convert.ToDecimal(item["other_nonoperating_income"].ToString()),
                OtherAssets = System.Convert.ToDecimal(item["pretax_income"].ToString()),
                TotalAssets = System.Convert.ToDecimal(item["income_tax"].ToString()),
                AccountsPayable = System.Convert.ToDecimal(item["net_income"].ToString()),
                TaxPayable = System.Convert.ToDecimal(item["eps_basic"].ToString()),
                ShortTermDebt = System.Convert.ToDecimal(item["eps_diluted"].ToString()),
                CurrentDeferredRevenue = System.Convert.ToDecimal(item["shares_basic"].ToString()),
                OtherCurrentLabilities = System.Convert.ToDecimal(item["shares_diluted"].ToString()),
                TotalCurrentLiabilities = System.Convert.ToDecimal(item["eps_diluted"].ToString()),
                LongTermDebt = System.Convert.ToDecimal(item["shares_basic"].ToString()),
                CapitalLeases = System.Convert.ToDecimal(item["shares_diluted"].ToString()),
                NonCurrentDeferredRevenue = System.Convert.ToDecimal(item["eps_diluted"].ToString()),
                OtherLiabilities = System.Convert.ToDecimal(item["shares_basic"].ToString()),
                TotalLiabilities = System.Convert.ToDecimal(item["shares_diluted"].ToString()),
                RetainedEarnings = System.Convert.ToDecimal(item["eps_diluted"].ToString()),
                CommonStock = System.Convert.ToDecimal(item["shares_basic"].ToString()),
                Aoci = System.Convert.ToDecimal(item["shares_diluted"].ToString()),
                ShareholdersEquity = System.Convert.ToDecimal(item["shares_basic"].ToString()),
                TotalLiabilitiesAndEquity = System.Convert.ToDecimal(item["shares_diluted"].ToString()),
                Uuid = Guid.NewGuid()
            };

            //var balanceAdded = genericDao.Add<BalanceSheet>(balance);

            return 1; //balanceAdded.Id;

        }
    }
}