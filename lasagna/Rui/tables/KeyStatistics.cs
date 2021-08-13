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
    class KeyStatistics
    {
        GenericDAO genericDao;
        public KeyStatistics(GenericDAO genericDaoOut)
        {
            genericDao = genericDaoOut;
        }

        public int insertKeyStatistics(String keyStatistics, int index)
        {

                var jsonCompanyList = JObject.Parse(keyStatistics);
                var companyArray = jsonCompanyList["data"];
            // vew numero de dados
                var companyData = companyArray.Children().ToList();
                Console.WriteLine(companyData.Count());

            //criar keyStatistic
                var item = companyArray["financials"]["annual"];
                var CompanyObject = new List<KeyStatistic>();

                    var keyStatistic = new KeyStatistic
                    {

                        Pe = System.Convert.ToDecimal(item["pe"][index].ToString()) ,
                        Pb = System.Convert.ToDecimal(item["pb"][index].ToString()) ,
                        Ps = System.Convert.ToDecimal(item["ps"][index].ToString()),
                        Evs = System.Convert.ToDecimal(item["ev_s"][index].ToString()),
                        Evebitda = System.Convert.ToDecimal(item["ev_ebitda"][index].ToString()) ,
                        Evebit = System.Convert.ToDecimal(item["ev_ebit"][index].ToString()) ,
                        Evpretax = System.Convert.ToDecimal(item["ev_pretax_inc"][index].ToString()),
                        Evfcf = System.Convert.ToDecimal(item["ev_fcf"][index].ToString()),
                        Roamedian = System.Convert.ToDecimal(item["roa_median"][index].ToString()),
                        Roemedian = System.Convert.ToDecimal(item["roe_median"][index].ToString()) ,
                        Roicmedian = System.Convert.ToDecimal(item["roic_median"][index].ToString()) ,
                        RevenueCagr = System.Convert.ToDecimal(item["revenue_cagr_10"][index].ToString()) ,
                        AssetsCagr = System.Convert.ToDecimal(item["total_assets_cagr_10"][index].ToString()) ,
                        Fcfcagr = System.Convert.ToDecimal(item["fcf_cagr_10"][index].ToString()) ,
                        Epscagr = System.Convert.ToDecimal(item["eps_diluted_cagr_10"][index].ToString()) ,
                        GrossProfitMedian = System.Convert.ToDecimal(item["gross_margin_median"][index].ToString()) ,
                        Ebitmedian = System.Convert.ToDecimal(item["operating_income_margin_median"][index].ToString()) ,
                        PreTaxIncomeMedian = System.Convert.ToDecimal(item["pretax_margin_median"][index].ToString()) ,
                        Fcfmedian = System.Convert.ToDecimal(item["fcf_margin_median"][index].ToString()) ,
                        AssetsEquityMedian = System.Convert.ToDecimal( item["assets_to_equity_median"][index].ToString()),
                        DebtEquityMedian = System.Convert.ToDecimal(item["debt_to_equity_median"][index].ToString()) ,
                        DebtAssetsMedian = System.Convert.ToDecimal(item["debt_to_assets_median"][index].ToString()) ,
                        Uuid = Guid.NewGuid()
                    };

            //var statisticAdded = genericDao.Add<KeyStatistic>(keyStatistic);

            return 1; //statisticAdded.Id;
            
        }
    }
}
