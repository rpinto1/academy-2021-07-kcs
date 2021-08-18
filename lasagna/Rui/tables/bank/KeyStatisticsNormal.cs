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
    class KeyStatisticsBank
    {
        GenericDAO genericDao;
        public KeyStatisticsBank(GenericDAO genericDaoOut)
        {
            genericDao = genericDaoOut;
        }

        public int insertKeyStatistics(String keyStatistics, int index, int companyId)
        {

                var jsonCompanyList = JObject.Parse(keyStatistics);
                var companyArray = jsonCompanyList["data"];

            //criar keyStatistic
                var item = companyArray["financials"]["annual"];
                var CompanyObject = new List<KeyStatistic>();

                    var keyStatistic = new KeyStatistic
                    {
                        CompanyId = companyId,
                        Roamedian = System.Convert.ToDecimal(item["roa_median"].ToString()),
                        Roemedian = System.Convert.ToDecimal(item["roe_median"].ToString()),
                        Roicmedian = System.Convert.ToDecimal(item["roic_median"].ToString()),
                        RevenueCagr = System.Convert.ToDecimal(item["revenue_cagr_10"][index].ToString()),
                        AssetsCagr = System.Convert.ToDecimal(item["total_assets_cagr_10"][index].ToString()),
                        Fcfcagr = System.Convert.ToDecimal(item["fcf_cagr_10"][index].ToString()),
                        Epscagr = System.Convert.ToDecimal(item["eps_diluted_cagr_10"][index].ToString()),
                        GrossProfitMedian = System.Convert.ToDecimal(item["gross_margin_median"].ToString()),
                        Ebitmedian = System.Convert.ToDecimal(item["operating_income_margin_median"].ToString()),
                        PreTaxIncomeMedian = System.Convert.ToDecimal(item["pretax_margin_median"].ToString()),
                        Fcfmedian = System.Convert.ToDecimal(item["fcf_margin_median"].ToString()),
                        AssetsEquityMedian = System.Convert.ToDecimal(item["assets_to_equity_median"].ToString()),
                        DebtEquityMedian = System.Convert.ToDecimal(item["debt_to_equity_median"].ToString()),
                        DebtAssetsMedian = System.Convert.ToDecimal(item["debt_to_assets_median"].ToString()),
                        Uuid = Guid.NewGuid()
                    };

            //var statisticAdded = genericDao.Add<KeyStatistic>(keyStatistic);

            return 1; //statisticAdded.Id;
            
        }
    }
}
