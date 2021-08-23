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

namespace Rui.tables
{
    class IncomeStatementsNormal
    {
        GenericDAO genericDao;
        public IncomeStatementsNormal(GenericDAO genericDaoOut)
    {
            genericDao = genericDaoOut;
    }
        public Task<IncomeStatement> insertIncomeStatements(String incomeStatements, int index)
        {

            var jsonCompanyList = JObject.Parse(incomeStatements);
            var companyArray = jsonCompanyList["data"];

            //criar keyStatistic
            var item = companyArray["financials"]["annual"];

            //Console.WriteLine(item["revenue"][index]);

            var income = new IncomeStatement
            {

                Revenue = Decimal.Parse(item["revenue"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                CostOfGoodsSold = Decimal.Parse(item["cogs"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                GrossProfit = Decimal.Parse(item["gross_profit"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                SalesGeneralAdministrative = Decimal.Parse(item["sga"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                Development = Decimal.Parse(item["rnd"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                OtherOperatingExpense = Decimal.Parse(item["other_opex"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                TotalOperatingExpenses = Decimal.Parse(item["total_opex"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                OperatingProfit = Decimal.Parse(item["operating_income"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                NetInterestIncome = Decimal.Parse(item["interest_income"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                OtherNonOperatingIncome = Decimal.Parse(item["other_nonoperating_income"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                PreTaxIncome = Decimal.Parse(item["pretax_income"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                IncomeTax = Decimal.Parse(item["income_tax"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                NetIncome = Decimal.Parse(item["net_income"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                Epsbasic = Decimal.Parse(item["eps_basic"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                Epsdiluted = Decimal.Parse(item["eps_diluted"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                SharesBasic = Decimal.Parse(item["shares_basic"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                SharesDiluted = Decimal.Parse(item["shares_diluted"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                Uuid = Guid.NewGuid()
            };

            return genericDao.AddAsync<IncomeStatement>(income);


        }
    }
}
