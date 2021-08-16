using KCSit.SalesforceAcademy.Kappify.DataAccess;
using KCSit.SalesforceAcademy.Lasagna.Data;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Raul
{
    class ProgramCashFlow
    {
        static void Main2(string[] args)
        {
            var genericDao = new GenericDAO();

            var files = new string[] { "dataMM", "dataAU", "dataCA", "dataLN", "dataNZ", "dataUS" };
            foreach (var file in files)
            {



                var CashFlowListFile = File.ReadAllText(@"C:\Users\User01\source\repos\rpinto1\academy-2021-07-kcs\lasagna\Rui\JsonFiles\" + file + ".txt");
                var jsonCashFlowList = JObject.Parse(CashFlowListFile);
                var CashFlowArray = jsonCashFlowList["data"]["financials"]["annual"];
                var CashFlowList = CashFlowArray.Children().ToList();
                Console.WriteLine(CashFlowList.Count());
                var CashFlowObject = new List<CashFlowStatement>();
                foreach (var item in CashFlowArray)
                {

                    Console.WriteLine(item["net_income"]);
                    Console.WriteLine(item["cfo_da"]);
                    Console.WriteLine(item["cfo_change_in_working_capital"]);
                    Console.WriteLine(item["cfo_deferred_tax"]);
                    Console.WriteLine(item["cfo_stock_comp"]);
                    Console.WriteLine(item["cfo_other_noncash_items"]);
                    Console.WriteLine(item["cf_cfo"]);
                    Console.WriteLine(item["cfi_ppe_net"]);
                    Console.WriteLine(item["cfi_acquisitions_net"]);
                    Console.WriteLine(item["cfi_investment_net"]);
                    Console.WriteLine(item["cfi_intangibles_net"]);
                    Console.WriteLine(item["cfi_other"]);
                    Console.WriteLine(item["cf_cfi"]);
                    Console.WriteLine(item["cff_common_stock_net"]);
                    Console.WriteLine(item["cff_debt_net"]);
                    Console.WriteLine(item["cff_dividend_paid"]);
                    Console.WriteLine(item["cff_other"]);
                    Console.WriteLine(item["cf_cff"]);
                    CashFlowObject.Add(new CashFlowStatement
                    {
                        NetIncome =System.Convert.ToDecimal(item["net_income"].ToString()) ,
                        DepreciationAmortization = System.Convert.ToDecimal(item["cfo_da"].ToString()),
                        ChangeInWorkingCapital = System.Convert.ToDecimal(item["cfo_change_in_working_capital"].ToString()),
                        ChangeInDeferredTax = System.Convert.ToDecimal(item["cfo_deferred_tax"].ToString()),
                        StockBasedCompensation = System.Convert.ToDecimal(item["cfo_stock_comp"].ToString()),
                        OtherOperations = System.Convert.ToDecimal(item["cfo_other_noncash_items"].ToString()),
                        CashFromOperations = System.Convert.ToDecimal(item["cf_cfo"].ToString()),
                        PropertyPlantEquipment = System.Convert.ToDecimal(item["cfi_ppe_net"].ToString()),
                        Acquisitions = System.Convert.ToDecimal(item["cfi_acquisitions_net"].ToString()),
                        Investements = System.Convert.ToDecimal(item["cfi_investment_net"].ToString()),
                        OtherInvesting = System.Convert.ToDecimal(item["cfi_other"].ToString()),
                        CashFromInvesting = System.Convert.ToDecimal(item["cf_cfi"].ToString()),
                        NetIssuanceOfCommonStock = System.Convert.ToDecimal(item["cff_common_stock_net"].ToString()),
                        NetIssuanceOfDebt = System.Convert.ToDecimal(item["cff_debt_net"].ToString()),
                        CashPaidForDividends = System.Convert.ToDecimal(item["cff_dividend_paid"].ToString()),
                        OtherFinancing = System.Convert.ToDecimal(item["cff_other"].ToString()),
                        CashFinancing = System.Convert.ToDecimal(item["cf_cff"].ToString()),
                        Uuid = Guid.NewGuid()
                    });

                }

                genericDao.AddRange<CashFlowStatement>(CashFlowObject);
            }

            Console.WriteLine("Enter user api Key");
            string apiKey = Console.ReadLine();

            var clientClass = new Client();

            IRestResponse responseList = clientClass.GetAll("https:public-api.quickfs.net/v1/companies?api_key=" + apiKey);

            var responseCashFlowList = JObject.Parse(responseList.Content)["data"];


            for (int i = 1; i < responseCashFlowList.ToObject<string[]>().Length; i++)
            {


                if (clientClass.CheckQuota(apiKey) < 2000)
                {

                    Environment.Exit(0);
                }

                IRestResponse response = clientClass.GetAll("https:public-api.quickfs.net/v1/data/all-data/" + responseCashFlowList[i].ToString() + "?api_key=" + apiKey);

                var responseJson = JObject.Parse(response.Content);
                var metadata = responseJson["data"]["metadata"];
                Console.WriteLine(metadata["name"].ToString());

                Console.WriteLine(metadata["sector"].ToString());
                int industryId = 0;

                if (metadata["sector"] == null)
                {
                    continue;
                }

                if (genericDao.Get(metadata["sector"].ToString()) == null)
                {
                    var index = genericDao.Add<Industry>(new Industry
                    {
                        Name = metadata["sector"].ToString(),
                        Uuid = Guid.NewGuid()
                    });

                    industryId = index.Id;
                    Console.WriteLine("Insert Industry");
                }
                else
                {
                    industryId = int.Parse(genericDao.Get(metadata["sector"].ToString()).Id.ToString());
                }

                if (metadata["industry"] == null)
                {
                    continue;
                }
                if (genericDao.GetSub(metadata["industry"].ToString()) == null)
                {
                    genericDao.Add<SubIndustry>(new SubIndustry
                    {
                        IndustryId = industryId,
                        Name = metadata["sindustry"].ToString(),
                        Uuid = Guid.NewGuid()
                    });
                    Console.WriteLine("Insert Sub");

                }



            }
        }
        }
    }
