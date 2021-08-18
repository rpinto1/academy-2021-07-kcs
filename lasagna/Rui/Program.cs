using KCSit.SalesforceAcademy.Kappify.DataAccess;
using KCSit.SalesforceAcademy.Lasagna.Data;
using Newtonsoft.Json.Linq;
using RestSharp;
using Rui.tables;
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
                Console.WriteLine(item);
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



                    if (yearIndex == count - 1)
                    {
                        keyStatistics.insertKeyStatistics(response.Content, yearIndex, companyId);
                    }
                    // para bancos

                    // para empresas normais 
                    var yearlyReport = new YearlyReport
                    {
                        Year = listYearInt.ElementAt(yearIndex),
                        CompanyId = companyId,
                        IncomeStatementId = income.insertIncomeStatements(response.Content, yearIndex),
                        BalanceSheetId = balance.insertBalanceSheets(response.Content, yearIndex),
                        CashFlowStatementId = cashFlow.InsertCashFlowStatements(response.Content, yearIndex),
                        KeyRatioId = ratios.InsertKeyRatios(response.Content, yearIndex),
                        Uuid = Guid.NewGuid()
                    };
                    //genericDao.Add<YearlyReport>(yearlyReport);

                }
            }








        }
    }
}
