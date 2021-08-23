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
using System.Threading.Tasks;

namespace Rui.tables.insurance
{
    class KeyStatisticsInsurance
    {
        GenericDAO genericDao;
        public KeyStatisticsInsurance(GenericDAO genericDaoOut)
        {
            genericDao = genericDaoOut;
        }

        public void insertKeyStatistics(String keyStatistics, int index, int companyId)
        {

                var jsonCompanyList = JObject.Parse(keyStatistics);
                var companyArray = jsonCompanyList["data"];

            //criar keyStatistic
                var item = companyArray["financials"]["annual"];
                var CompanyObject = new List<KeyStatistic>();

                    var keyStatistic = new KeyStatistic
                    {
                        CompanyId = companyId,
                        Roamedian = Decimal.Parse(item["roa_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        Roemedian = Decimal.Parse(item["roe_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        Roimedian = Decimal.Parse(item["roi_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        RevenueCagr = Decimal.Parse(item["revenue_cagr_10"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        PermiumCagr = Decimal.Parse(item["premiums_cagr_10"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        AssetsCagr = Decimal.Parse(item["total_assets_cagr_10"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        Epscagr = Decimal.Parse(item["eps_diluted_cagr_10"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        UnderwritingMedian = Decimal.Parse(item["underwriting_margin_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        PreTaxIncomeMedian = Decimal.Parse(item["pretax_margin_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        AssetsEquityMedian = Decimal.Parse(item["assets_to_equity_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        EquityAssetsMedian = Decimal.Parse(item["equity_to_assets_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        Uuid = Guid.NewGuid()
                    };

            genericDao.AddAsync<KeyStatistic>(keyStatistic);

            
        }
    }
}
