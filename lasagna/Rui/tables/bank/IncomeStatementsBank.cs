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
    class IncomeStatementsBank
    {
        GenericDAO genericDao;
        public IncomeStatementsBank(GenericDAO genericDaoOut)
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

                TotalInterestIncome = Decimal.Parse(item["total_interest_income"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                TotalInterestExpense = Decimal.Parse(item["total_interest_expense"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                NetInterestIncomeBank = Decimal.Parse(item["net_interest_income"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                TotalNoninterestRevenue = Decimal.Parse(item["total_noninterest_revenue"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                TotalNonInterestExpense = Decimal.Parse(item["total_noninterest_expense"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                PreTaxIncome = Decimal.Parse(item["pretax_income"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                CreditLossesProvision = Decimal.Parse(item["credit_losses_provision"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                NetInterestAclp = Decimal.Parse(item["net_interest_income_after_credit_losses_provision"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
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
