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

namespace Rui.tables.bank

{
    class KeyStatisticsBank
    {
        GenericDAO genericDao;
        public KeyStatisticsBank(GenericDAO genericDaoOut)
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
                        NetInterestIncomeCagr = Decimal.Parse(item["net_interest_income_cagr_10"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        GrossLoansCagr = Decimal.Parse(item["loans_gross_cagr_10"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        EarningAssetsCagr = Decimal.Parse(item["earning_assets_cagr_10"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        DepositsCagr = Decimal.Parse(item["deposits_cagr_10"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        Nimmedian = Decimal.Parse(item["nim_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        AssetsEquityMedian = Decimal.Parse(item["assets_to_equity_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        EarningAemedian = Decimal.Parse(item["earning_assets_to_equity_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        LoanLossRtlmedian = Decimal.Parse(item["loan_loss_reserve_to_loans_median"]?.ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        Uuid = Guid.NewGuid()
                    };

             genericDao.AddAsync<KeyStatistic>(keyStatistic);

            
        }
    }
}
