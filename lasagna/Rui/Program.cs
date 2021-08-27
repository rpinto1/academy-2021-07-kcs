using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.DataAccess;
using Newtonsoft.Json.Linq;
using RestSharp;
using Rui.tables;
using Rui.tables.bank;
using Rui.tables.insurance;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rui
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Enter user api Key");
            string apiKey = Console.ReadLine();

            var clientClass = new Client();

            var genericDao = new GenericDAO();
            var searchDao = new SearchDAO();

            var industries = new Industries();

            //industries.InsertIndustries(genericDao);
            var companies = new CompaniesCode();

            //companies.InsertCompanies(genericDao);
            //var index = new CompanyIndexCode();

            //index.InsertCompanies(genericDao);

            var income = new IncomeStatementsNormal(genericDao);
            var balance = new BalanceSheetsNormal(genericDao);
            var keyStatistics = new KeyStatisticsNormal(genericDao);
            var cashFlow = new CashFlowStatementsNormal(genericDao);
            var ratios = new KeyRatiosNormal(genericDao);

            var incomeBank = new IncomeStatementsBank(genericDao);
            var balanceBank = new BalanceSheetsBank(genericDao);
            var keyStatisticsBank = new KeyStatisticsBank(genericDao);
            var cashFlowBank = new CashFlowStatementsBank(genericDao);
            var ratiosBank = new KeyRatiosBank(genericDao);


            var incomeInsurance = new IncomeStatementsInsurance(genericDao);
            var balanceInsurance = new BalanceSheetsInsurance(genericDao);
            var keyStatisticsInsurance = new KeyStatisticsInsurance(genericDao);
            var cashFlowInsurance = new CashFlowStatementsInsurance(genericDao);
            var ratiosInsurance = new KeyRatiosInsurance(genericDao);

            // CODE to reference, Not Used
            //IRestResponse companyJson = clientClass.GetAll("https://public-api.quickfs.net/v1/companies?api_key=" + apiKey);
            //var companyListObject = JObject.Parse(companyJson.Content)["data"];
            //int? companyId = searchDao.Get(company);

            //if (companyId == null)
            //{
            //    continue;
            //}
            //File.WriteAllText(@"C:\Users\User01\source\repos\rpinto1\academy-2021-07-kcs\lasagna\Rui\JsonFiles\index.txt", i.ToString());
            //File.ReadAllText(@"C:\Users\User01\source\repos\rpinto1\academy-2021-07-kcs\lasagna\Rui\JsonFiles\index.txt");


            var companyBD = genericDao.GetAll<Company>();

            //GetIndex
            var index = genericDao.GetAll<KeyStatistic>().Count();  
            var counter = clientClass.CheckQuota(apiKey);
            Console.WriteLine(companyBD[index].Name);
            for (int i = index; i < companyBD.Count(); i++)
            {
                var item = companyBD[i];
                Console.WriteLine(item.Ticker);
                var company = item.Ticker.ToString();

                var companyId = item.Id;
                Console.WriteLine(companyId);

                if (counter < 50)
                {
                    Console.WriteLine("Parou em " + item + " Numero: " + i);           
                    Environment.Exit(20);
                }
                counter -= 10;

                IRestResponse response = clientClass.GetAll("https://public-api.quickfs.net/v1/data/all-data/" + company + "?api_key=" + apiKey);
                Console.WriteLine(response.ResponseUri);
                
                var jsonCompanyList = JObject.Parse(response.Content);
                //Get the year

                var year = jsonCompanyList["data"]["financials"]["annual"]["period_end_date"];

                var listYear = year.Children().ToList().Values<string>().ToList();
                var listYearInt = listYear.Select(fullYear => int.Parse(fullYear.Split("-")[0]));
                var count = year.Children().ToList().Count();
                Console.WriteLine(count);

            
            for (int yearIndex = 0; yearIndex < count; yearIndex++)
                {



                    Task<KeyRatio> keyTask;
                    Task<IncomeStatement> incomeTask;
                    Task<BalanceSheet> balanceTask;
                    Task<CashFlowStatement> cashTask;

                    YearlyReport yearlyReport;


                    // para bancos
                    switch (item.CompanyType)
                    {
                        case "bank":
                            if (yearIndex == count - 1)
                            {
                                keyStatisticsBank.insertKeyStatistics(response.Content, yearIndex, companyId);
                            }
                            balanceTask = balanceBank.insertBalanceSheets(response.Content, yearIndex);
                            keyTask = ratiosBank.InsertKeyRatios(response.Content, yearIndex);
                            incomeTask = incomeBank.insertIncomeStatements(response.Content, yearIndex);
                            
                            cashTask = cashFlowBank.InsertCashFlowStatements(response.Content, yearIndex);

                            var taskArrayBank = new Task[] { keyTask, incomeTask, balanceTask, cashTask };
                            
                            Task.WaitAll(taskArrayBank);
                            Console.WriteLine(keyTask.Result.Id);

                            break;

                        case "normal":
                            if (yearIndex == count - 1)
                            {
                                keyStatistics.insertKeyStatistics(response.Content, yearIndex, companyId);
                            }
                            balanceTask = balance.insertBalanceSheets(response.Content, yearIndex);
                            keyTask = ratios.InsertKeyRatios(response.Content, yearIndex);
                            incomeTask = income.insertIncomeStatements(response.Content, yearIndex);
                            
                            cashTask = cashFlow.InsertCashFlowStatements(response.Content, yearIndex);

                            var taskArrayNormal = new Task[] { keyTask, incomeTask, balanceTask, cashTask };
                            Task.WaitAll(taskArrayNormal);
                            Console.WriteLine(keyTask.Result.Id);

                            break;
                        case "insurance":
                            if (yearIndex == count - 1)
                            {
                                keyStatisticsInsurance.insertKeyStatistics(response.Content, yearIndex, companyId);
                            }
                            keyTask = ratiosInsurance.InsertKeyRatios(response.Content, yearIndex);
                            incomeTask = incomeInsurance.insertIncomeStatements(response.Content, yearIndex);
                            balanceTask = balanceInsurance.insertBalanceSheets(response.Content, yearIndex);
                            cashTask = cashFlowInsurance.InsertCashFlowStatements(response.Content, yearIndex);

                            var taskArrayInsurance = new Task[] { keyTask, incomeTask, balanceTask, cashTask };
                            Task.WaitAll(taskArrayInsurance);
                            Console.WriteLine(keyTask.Result.Id);
                            break;
                        default:
                            Console.WriteLine("break");
                            keyTask = ratios.InsertKeyRatios(response.Content, yearIndex);
                            incomeTask = income.insertIncomeStatements(response.Content, yearIndex);
                            balanceTask = balance.insertBalanceSheets(response.Content, yearIndex);
                            cashTask = cashFlow.InsertCashFlowStatements(response.Content, yearIndex);
                            break;
                    }

                    yearlyReport = new YearlyReport
                    {
                        Year = listYearInt.ElementAt(yearIndex),
                        CompanyId = companyId,
                        IncomeStatementId = incomeTask.Result.Id,
                        BalanceSheetId = balanceTask.Result.Id,
                        CashFlowStatementId = cashTask.Result.Id,
                        KeyRatioId = keyTask.Result.Id,
                        Uuid = Guid.NewGuid()
                    };
                    genericDao.Add<YearlyReport>(yearlyReport);


                }
            }








        }
    }
}
