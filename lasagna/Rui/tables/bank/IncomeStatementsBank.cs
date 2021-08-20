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
    class IncomeStatementsBank
    {
        GenericDAO genericDao;
        public IncomeStatementsBank(GenericDAO genericDaoOut)
    {
            genericDao = genericDaoOut;
    }
        public int insertIncomeStatements(String incomeStatements, int index)
        {

            var jsonCompanyList = JObject.Parse(incomeStatements);
            var companyArray = jsonCompanyList["data"];

            //criar keyStatistic
            var item = companyArray["financials"]["annual"];

            //Console.WriteLine(item["revenue"][index]);

            var income = new IncomeStatement
            {

                TotalInterestIncome = System.Convert.ToDecimal(item["total_interest_income"][index].ToString()),
                TotalInterestExpense = System.Convert.ToDecimal(item["total_interest_expense"][index].ToString()),
                NetInterestIncomeBank = System.Convert.ToDecimal(item["net_interest_income"][index].ToString()),
                TotalNoninterestRevenue = System.Convert.ToDecimal(item["total_noninterest_revenue"][index].ToString()),
                TotalNonInterestExpense = System.Convert.ToDecimal(item["total_noninterest_expense"][index].ToString()),
                PreTaxIncome = System.Convert.ToDecimal(item["pretax_income"][index].ToString()),
                CreditLossesProvision = System.Convert.ToDecimal(item["credit_losses_provision"][index].ToString()),
                NetInterestAclp = System.Convert.ToDecimal(item["net_interest_income_after_credit_losses_provision"][index].ToString()),
                IncomeTax = System.Convert.ToDecimal(item["income_tax"][index].ToString()),
                NetIncome = System.Convert.ToDecimal(item["net_income"][index].ToString()),
                Epsbasic = System.Convert.ToDecimal(item["eps_basic"][index].ToString()),
                Epsdiluted = System.Convert.ToDecimal(item["eps_diluted"][index].ToString()),
                SharesBasic = System.Convert.ToDecimal(item["shares_basic"][index].ToString()),
                SharesDiluted = System.Convert.ToDecimal(item["shares_diluted"][index].ToString()),
                Uuid = Guid.NewGuid()
            };

            var incomeAdded = genericDao.Add<IncomeStatement>(income);

            return incomeAdded.Id;

        }
    }
}
