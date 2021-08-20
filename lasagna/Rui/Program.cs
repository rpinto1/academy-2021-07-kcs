﻿using KCSit.SalesforceAcademy.Kappify.DataAccess;
using KCSit.SalesforceAcademy.Lasagna.Data;
using Newtonsoft.Json.Linq;
using RestSharp;
using Rui.tables;
using Rui.tables.bank;
using Rui.tables.insurance;
using System;
using System.IO;
using System.Linq;

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


                var jsonCompanyList = JObject.Parse(response.Content);
                //Get the year

                var year = jsonCompanyList["data"]["financials"]["annual"]["period_end_date"];

                var listYear = year.Children().ToList().Values<string>().ToList();
                var listYearInt = listYear.Select(fullYear => int.Parse(fullYear.Split("-")[0]));
                var count = year.Children().ToList().Count();
                Console.WriteLine(count);

                for (int yearIndex = 0; yearIndex < count; yearIndex++)
                {

                    YearlyReport yearlyReport;


                    // para bancos
                    switch (item.CompanyType)
                    {
                        case "bank":
                            if (yearIndex == count - 1)
                            {
                                keyStatisticsBank.insertKeyStatistics(response.Content, yearIndex, companyId);
                            }
                            yearlyReport = new YearlyReport
                            {
                                Year = listYearInt.ElementAt(yearIndex),
                                CompanyId = companyId,
                                IncomeStatementId = incomeBank.insertIncomeStatements(response.Content, yearIndex),
                                BalanceSheetId = balanceBank.insertBalanceSheets(response.Content, yearIndex),
                                CashFlowStatementId = cashFlowBank.InsertCashFlowStatements(response.Content, yearIndex),
                                KeyRatioId = ratiosBank.InsertKeyRatios(response.Content, yearIndex),
                                Uuid = Guid.NewGuid()
                            };
                            genericDao.Add<YearlyReport>(yearlyReport);
                            break;

                        case "normal":
                            if (yearIndex == count - 1)
                            {
                                keyStatistics.insertKeyStatistics(response.Content, yearIndex, companyId);
                            }
                            yearlyReport = new YearlyReport
                            {
                                Year = listYearInt.ElementAt(yearIndex),
                                CompanyId = companyId,
                                IncomeStatementId = income.insertIncomeStatements(response.Content, yearIndex),
                                BalanceSheetId = balance.insertBalanceSheets(response.Content, yearIndex),
                                CashFlowStatementId = cashFlow.InsertCashFlowStatements(response.Content, yearIndex),
                                KeyRatioId = ratios.InsertKeyRatios(response.Content, yearIndex),
                                Uuid = Guid.NewGuid()
                            };
                            genericDao.Add<YearlyReport>(yearlyReport);
                            break;
                        case "insurance":
                            if (yearIndex == count - 1)
                            {
                                keyStatisticsInsurance.insertKeyStatistics(response.Content, yearIndex, companyId);
                            }
                            yearlyReport = new YearlyReport
                            {
                                Year = listYearInt.ElementAt(yearIndex),
                                CompanyId = companyId,
                                IncomeStatementId = incomeInsurance.insertIncomeStatements(response.Content, yearIndex),
                                BalanceSheetId = balanceInsurance.insertBalanceSheets(response.Content, yearIndex),
                                CashFlowStatementId = cashFlowInsurance.InsertCashFlowStatements(response.Content, yearIndex),
                                KeyRatioId = ratiosInsurance.InsertKeyRatios(response.Content, yearIndex),
                                Uuid = Guid.NewGuid()
                            };
                            genericDao.Add<YearlyReport>(yearlyReport);

                            break;
                        default:
                            Console.WriteLine("break");
                            break;
                    }


                }
            }








        }
    }
}
