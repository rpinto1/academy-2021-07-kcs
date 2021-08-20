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
    class IncomeStatementsInsurance
    {
        GenericDAO genericDao;
        public IncomeStatementsInsurance(GenericDAO genericDaoOut)
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

                TotalPremiums = System.Convert.ToDecimal(item["premiums_earned"][index].ToString()),
                NetIvestmentIncome = System.Convert.ToDecimal(item["net_investment_income"][index].ToString()),
                FeesOtherIncome = System.Convert.ToDecimal(item["fees_and_other_income"][index].ToString()),
                Revenue = System.Convert.ToDecimal(item["revenue"][index].ToString()),


                SalesGeneralAdministrative = System.Convert.ToDecimal(item["sga"][index].ToString()),
                PolicyClaims = System.Convert.ToDecimal(item["net_policyholder_claims_expense"][index].ToString()),
                PolicyExpense = System.Convert.ToDecimal(item["policy_acquisition_expense"][index].ToString()),
                InterestExpense = System.Convert.ToDecimal(item["interest_expense_insurance"][index].ToString()),

                PreTaxIncome = System.Convert.ToDecimal(item["pretax_income"][index].ToString()),


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
