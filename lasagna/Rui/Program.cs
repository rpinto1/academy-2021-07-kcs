using KCSit.SalesforceAcademy.Kappify.DataAccess;
using RestSharp;
using Rui.tables;
using System;

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


            var industries = new Industries();

            //industries.InsertIndustries(genericDao);
            var companies = new CompaniesCode();

            //companies.InsertCompanies(genericDao);
            var index = new CompanyIndexCode();

            //index.InsertCompanies(genericDao);

            var income = new IncomeStatements(genericDao);
            IRestResponse response = clientClass.GetAll("https://public-api.quickfs.net/v1/data/all-data/KO:US?api_key=" + apiKey);
            income.insertIncomeStatements(response.Content, 0);



        }
    }
}
