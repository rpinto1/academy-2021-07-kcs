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
                        NetInterestIncomeCagr = System.Convert.ToDecimal(item["net_interest_income_cagr_10"][index].ToString()),
                        GrossLoansCagr = System.Convert.ToDecimal(item["loans_gross_cagr_10"][index].ToString()),
                        EarningAssetsCagr = System.Convert.ToDecimal(item["earning_assets_cagr_10"][index].ToString()),
                        DepositsCagr = System.Convert.ToDecimal(item["deposits_cagr_10"][index].ToString()),
                        Nimmedian = System.Convert.ToDecimal(item["nim_median"].ToString()),
                        AssetsEquityMedian = System.Convert.ToDecimal(item["assets_to_equity_median"].ToString()),
                        EarningAemedian = System.Convert.ToDecimal(item["earning_assets_to_equity_median"].ToString()),
                        LoanLossRtlmedian = System.Convert.ToDecimal(item["loan_loss_reserve_to_loans_median"].ToString()),
                        Uuid = Guid.NewGuid()
                    };

            var statisticAdded = genericDao.Add<KeyStatistic>(keyStatistic);

            return statisticAdded.Id;
            
        }
    }
}
