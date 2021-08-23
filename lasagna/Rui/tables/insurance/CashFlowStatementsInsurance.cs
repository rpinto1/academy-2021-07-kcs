using KCSit.SalesforceAcademy.Kappify.DataAccess;
using KCSit.SalesforceAcademy.Lasagna.Data;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace Rui.tables.insurance
{
    class CashFlowStatementsInsurance
    {
        GenericDAO genericDao;
        public CashFlowStatementsInsurance(GenericDAO genericDaoOut)
        {
            genericDao = genericDaoOut;
        }
        public Task<CashFlowStatement> InsertCashFlowStatements(String cashFlowStatements, int index)
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
                        NetIncome =Decimal.Parse(item["net_income"][index].ToString()) ,
                        DepreciationAmortization = Decimal.Parse(item["cfo_da"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        ChangeInWorkingCapital = Decimal.Parse(item["cfo_change_in_working_capital"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        ChangeInDeferredTax = Decimal.Parse(item["cfo_deferred_tax"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        StockBasedCompensation = Decimal.Parse(item["cfo_stock_comp"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        OtherOperations = Decimal.Parse(item["cfo_other_noncash_items"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        CashFromOperations = Decimal.Parse(item["cf_cfo"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        PropertyPlantEquipment = Decimal.Parse(item["cfi_ppe_net"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        Acquisitions = Decimal.Parse(item["cfi_acquisitions_net"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        Investements = Decimal.Parse(item["cfi_investment_net"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        OtherInvesting = Decimal.Parse(item["cfi_other"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        CashFromInvesting = Decimal.Parse(item["cf_cfi"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        NetIssuanceOfCommonStock = Decimal.Parse(item["cff_common_stock_net"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        NetIssuanceOfDebt = Decimal.Parse(item["cff_debt_net"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        CashPaidForDividends = Decimal.Parse(item["cff_dividend_paid"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        OtherFinancing = Decimal.Parse(item["cff_other"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        CashFinancing = Decimal.Parse(item["cf_cff"]?[index].ToString() ?? "0", System.Globalization.NumberStyles.Float),
                        Uuid = Guid.NewGuid()
                    };

    

            return genericDao.AddAsync<CashFlowStatement>(CashFlowObject);

        }

            
        }
        
    }
