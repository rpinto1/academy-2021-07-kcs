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
    class IncomeStatements
    {
        GenericDAO genericDao;
        public IncomeStatements(GenericDAO genericDaoOut)
    {
            genericDao = genericDaoOut;
    }
        public int insertIncomeStatements(String incomeStatements, int index)
        {

            var jsonCompanyList = JObject.Parse(incomeStatements);
            var companyArray = jsonCompanyList["data"];

            //criar keyStatistic
            var item = companyArray["financials"]["annual"];
            var CompanyObject = new List<KeyStatistic>();

            Console.WriteLine(item["revenue"][index]);

            var income = new IncomeStatement
            {

                Revenue = System.Convert.ToDecimal(item["revenue"][index].ToString()),
                CostOfGoodsSold = System.Convert.ToDecimal(item["cogs"][index].ToString()),
                GrossProfit = System.Convert.ToDecimal(item["gross_profit"][index].ToString()),
                SalesGeneralAdministrative = System.Convert.ToDecimal(item["sga"][index].ToString()),
                Development = System.Convert.ToDecimal(item["rnd"][index].ToString()),
                OtherOperatingExpense = System.Convert.ToDecimal(item["other_opex"].ToString()),
                TotalOperatingExpenses = System.Convert.ToDecimal(item["total_opex"].ToString()),
                OperatingProfit = System.Convert.ToDecimal(item["operating_income"].ToString()),
                NetInterestIncome = System.Convert.ToDecimal(item["interest_income"].ToString()),
                OtherNonOperatingIncome = System.Convert.ToDecimal(item["other_nonoperating_income"].ToString()),
                PreTaxIncome = System.Convert.ToDecimal(item["pretax_income"].ToString()),
                IncomeTax = System.Convert.ToDecimal(item["income_tax"].ToString()),
                NetIncome = System.Convert.ToDecimal(item["net_income"].ToString()),
                Epsbasic = System.Convert.ToDecimal(item["eps_basic"].ToString()),
                Epsdiluted = System.Convert.ToDecimal(item["eps_diluted"].ToString()),
                SharesBasic = System.Convert.ToDecimal(item["shares_basic"].ToString()),
                SharesDiluted = System.Convert.ToDecimal(item["shares_diluted"].ToString()),
                Uuid = Guid.NewGuid()
            };

            //var incomeAdded = genericDao.Add<IncomeStatement>(income);

            return 1; //incomeAdded.Id;

        }
    }
}
