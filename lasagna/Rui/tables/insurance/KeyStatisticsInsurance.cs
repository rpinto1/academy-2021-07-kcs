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
    class KeyStatisticsInsurance
    {
        GenericDAO genericDao;
        public KeyStatisticsInsurance(GenericDAO genericDaoOut)
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
                        Roimedian = System.Convert.ToDecimal(item["roi_median"].ToString()),
                        RevenueCagr = System.Convert.ToDecimal(item["revenue_cagr_10"][index].ToString()),
                        PermiumCagr = System.Convert.ToDecimal(item["premiums_cagr_10"][index].ToString()),
                        AssetsCagr = System.Convert.ToDecimal(item["total_assets_cagr_10"][index].ToString()),
                        Epscagr = System.Convert.ToDecimal(item["eps_diluted_cagr_10"][index].ToString()),
                        UnderwritingMedian = System.Convert.ToDecimal(item["underwriting_margin_median"].ToString()),
                        PreTaxIncomeMedian = System.Convert.ToDecimal(item["pretax_margin_median"].ToString()),
                        AssetsEquityMedian = System.Convert.ToDecimal(item["assets_to_equity_median"].ToString()),
                        EquityAssetsMedian = System.Convert.ToDecimal(item["equity_to_assets_median"].ToString()),
                        Uuid = Guid.NewGuid()
                    };

            var statisticAdded = genericDao.Add<KeyStatistic>(keyStatistic);

            return statisticAdded.Id;
            
        }
    }
}
