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
        public int insertIncomeStatements(String IncomeStatements, int index)
        {

            var jsonCompanyList = JObject.Parse(IncomeStatements);
            var companyArray = jsonCompanyList["data"];

            //criar keyStatistic
            var item = companyArray["financials"]["annual"];
            var CompanyObject = new List<KeyStatistic>();

            Console.WriteLine(item["revenue"][index]);

            var income = new IncomeStatement
            {

                Revenue = System.Convert.ToDecimal(item["pe"][index].ToString()),
                CostOfGoodsSold = System.Convert.ToDecimal(item["pb"][index].ToString()),
                GrossProfit = System.Convert.ToDecimal(item["ps"][index].ToString()),
                SalesGeneralAdministrative = System.Convert.ToDecimal(item["ev_s"][index].ToString()),
                // = System.Convert.ToDecimal(item["ev_ebitda"][index].ToString()),
                //Evebit = System.Convert.ToDecimal(item["ev_ebit"].ToString()),
                //Evpretax = System.Convert.ToDecimal(item["ev_pretax_inc"].ToString()),
                //Evfcf = System.Convert.ToDecimal(item["ev_fcf"].ToString()),
                //Roamedian = System.Convert.ToDecimal(item["roa_median"].ToString()),
                //Roemedian = System.Convert.ToDecimal(item["roe_median"].ToString()),
                //Roicmedian = System.Convert.ToDecimal(item["roic_median"].ToString()),
                //RevenueCagr = System.Convert.ToDecimal(item["revenue_cagr_10"].ToString()),
                //AssetsCagr = System.Convert.ToDecimal(item["total_assets_cagr_10"].ToString()),
                //Fcfcagr = System.Convert.ToDecimal(item["fcf_cagr_10"].ToString()),
                //Epscagr = System.Convert.ToDecimal(item["eps_diluted_cagr_10"].ToString()),
                //GrossProfitMedian = System.Convert.ToDecimal(item["gross_margin_median"].ToString()),
                //Ebitmedian = System.Convert.ToDecimal(item["operating_income_margin_median"].ToString()),
                //PreTaxIncomeMedian = System.Convert.ToDecimal(item["pretax_margin_median"].ToString()),
                //Fcfmedian = System.Convert.ToDecimal(item["fcf_margin_median"].ToString()),
                //AssetsEquityMedian = System.Convert.ToDecimal(item["assets_to_equity_median"].ToString()),
                //DebtEquityMedian = System.Convert.ToDecimal(item["debt_to_equity_median"].ToString()),
                //DebtAssetsMedian = System.Convert.ToDecimal(item["debt_to_assets_median"].ToString()),
                Uuid = Guid.NewGuid()
            };

            //var incomeAdded = genericDao.Add<KeyStatistic>(keyStatistic);

            return 1; //incomeAdded.Id;

        }
    }
}
