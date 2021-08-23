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
    class IncomeStatementsInsurance
    {
        GenericDAO genericDao;
        public IncomeStatementsInsurance(GenericDAO genericDaoOut)
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

                TotalPremiums = Decimal.Parse(item["premiums_earned"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                NetIvestmentIncome = Decimal.Parse(item["net_investment_income"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                FeesOtherIncome = Decimal.Parse(item["fees_and_other_income"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                Revenue = Decimal.Parse(item["revenue"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),


                SalesGeneralAdministrative = Decimal.Parse(item["sga"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                PolicyClaims = Decimal.Parse(item["net_policyholder_claims_expense"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                PolicyExpense = Decimal.Parse(item["policy_acquisition_expense"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                InterestExpense = Decimal.Parse(item["interest_expense_insurance"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),

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
