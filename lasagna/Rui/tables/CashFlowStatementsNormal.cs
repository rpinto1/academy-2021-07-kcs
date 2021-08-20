using KCSit.SalesforceAcademy.Kappify.DataAccess;
using KCSit.SalesforceAcademy.Lasagna.Data;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rui.tables
{
    class CashFlowStatementsNormal
    {
        GenericDAO genericDao;
        public CashFlowStatementsNormal(GenericDAO genericDaoOut)
        {
            genericDao = genericDaoOut;
        }
        public int InsertCashFlowStatements(String cashFlowStatements, int index)
        {

            var jsonCompanyList = JObject.Parse(cashFlowStatements);
            var companyArray = jsonCompanyList["data"];

            //criar keyStatistic
            var item = companyArray["financials"]["annual"];

                    //Console.WriteLine(item["net_income"]);
                    //Console.WriteLine(item["cfo_da"]);
                    //Console.WriteLine(item["cfo_change_in_working_capital"]);
                    //Console.WriteLine(item["cfo_deferred_tax"]);
                    //Console.WriteLine(item["cfo_stock_comp"]);
                    //Console.WriteLine(item["cfo_other_noncash_items"]);
                    //Console.WriteLine(item["cf_cfo"]);
                    //Console.WriteLine(item["cfi_ppe_net"]);
                    //Console.WriteLine(item["cfi_acquisitions_net"]);
                    //Console.WriteLine(item["cfi_investment_net"]);
                    //Console.WriteLine(item["cfi_intangibles_net"]);
                    //Console.WriteLine(item["cfi_other"]);
                    //Console.WriteLine(item["cf_cfi"]);
                    //Console.WriteLine(item["cff_common_stock_net"]);
                    //Console.WriteLine(item["cff_debt_net"]);
                    //Console.WriteLine(item["cff_dividend_paid"]);
                    //Console.WriteLine(item["cff_other"]);
                    //Console.WriteLine(item["cf_cff"]);
                     var CashFlowObject = new CashFlowStatement
                    {
                        NetIncome =System.Convert.ToDecimal(item["net_income"][index].ToString()) ,
                        DepreciationAmortization = System.Convert.ToDecimal(item["cfo_da"][index].ToString()),
                        ChangeInWorkingCapital = System.Convert.ToDecimal(item["cfo_change_in_working_capital"][index].ToString()),
                        ChangeInDeferredTax = System.Convert.ToDecimal(item["cfo_deferred_tax"][index].ToString()),
                        StockBasedCompensation = System.Convert.ToDecimal(item["cfo_stock_comp"][index].ToString()),
                        OtherOperations = System.Convert.ToDecimal(item["cfo_other_noncash_items"][index].ToString()),
                        CashFromOperations = System.Convert.ToDecimal(item["cf_cfo"][index].ToString()),
                        PropertyPlantEquipment = System.Convert.ToDecimal(item["cfi_ppe_net"][index].ToString()),
                        Acquisitions = System.Convert.ToDecimal(item["cfi_acquisitions_net"][index].ToString()),
                        Investements = System.Convert.ToDecimal(item["cfi_investment_net"][index].ToString()),
                        OtherInvesting = System.Convert.ToDecimal(item["cfi_other"][index].ToString()),
                        CashFromInvesting = System.Convert.ToDecimal(item["cf_cfi"][index].ToString()),
                        NetIssuanceOfCommonStock = System.Convert.ToDecimal(item["cff_common_stock_net"][index].ToString()),
                        NetIssuanceOfDebt = System.Convert.ToDecimal(item["cff_debt_net"][index].ToString()),
                        CashPaidForDividends = System.Convert.ToDecimal(item["cff_dividend_paid"][index].ToString()),
                        OtherFinancing = System.Convert.ToDecimal(item["cff_other"][index].ToString()),
                        CashFinancing = System.Convert.ToDecimal(item["cf_cff"][index].ToString()),
                        Uuid = Guid.NewGuid()
                    };

     

                genericDao.Add<CashFlowStatement>(CashFlowObject);


            var cashFlowAdded = genericDao.Add<CashFlowStatement>(CashFlowObject);

            return cashFlowAdded.Id;
        }

            
        }
        
    }
