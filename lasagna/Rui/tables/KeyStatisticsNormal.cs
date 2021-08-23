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
using System.Linq;

namespace Rui.tables
{
    class KeyStatisticsNormal
    {
        GenericDAO genericDao;
        public KeyStatisticsNormal(GenericDAO genericDaoOut)
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
                        Roicmedian = Decimal.Parse(item["roic_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        RevenueCagr = Decimal.Parse(item["revenue_cagr_10"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        AssetsCagr = Decimal.Parse(item["total_assets_cagr_10"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        Fcfcagr = Decimal.Parse(item["fcf_cagr_10"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        Epscagr = Decimal.Parse(item["eps_diluted_cagr_10"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        GrossProfitMedian = Decimal.Parse(item["gross_margin_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        Ebitmedian = Decimal.Parse(item["operating_income_margin_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        PreTaxIncomeMedian = Decimal.Parse(item["pretax_margin_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        Fcfmedian = Decimal.Parse(item["fcf_margin_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        AssetsEquityMedian = Decimal.Parse(item["assets_to_equity_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        DebtEquityMedian = Decimal.Parse(item["debt_to_equity_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        DebtAssetsMedian = Decimal.Parse(item["debt_to_assets_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        Uuid = Guid.NewGuid()
                    };

            genericDao.AddAsync<KeyStatistic>(keyStatistic);

            
            
        }
    }
}
