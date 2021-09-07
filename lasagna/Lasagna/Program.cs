using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.DataAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rui;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lasagna
{
    class Program
    {
        static void Main(string[] args)
        {
            // insert yahoo tickers
            var clientClass = new Client();

            var genericDao = new GenericDAO();
            var searchDao = new SearchDAO();



            //var companyBD = genericDao.GetAll<Company>();
            //var listUpdatedCompanys = new List<Company>();

            //for (int companyIndex = 12500; companyIndex < companyBD.Count; companyIndex++)
            //{
            //    var company = companyBD[companyIndex];
            //    string tickerYahoo = "";
            //    string canadaYahoo = "";
            //    string extraTicker = "";
            //    switch (company.CountryId)
            //    {
            //        // US
            //        case 1:
            //            tickerYahoo = company.Symbol;
            //            break;
            //        // CA
            //        case 2:
            //            canadaYahoo = company.Symbol + ".cn";
            //            tickerYahoo = company.Symbol;
            //            extraTicker = company.Symbol + ".V";
            //            break;
            //        // AU
            //        case 3:
            //            tickerYahoo = company.Symbol + ".AX";
            //            break;
            //        // NZ
            //        case 4:
            //            tickerYahoo = company.Symbol + ".nz";
            //            break;
            //        //MM
            //        case 5:
            //            tickerYahoo = company.Symbol + ".mx";
            //            break;
            //        // LN
            //        case 6:
            //            tickerYahoo = company.Symbol + ".L";
            //            break;

            //        default:
            //            Console.WriteLine("Hello");
            //            break;
            //    }

            //    var response = clientClass.GetAll("https://query1.finance.yahoo.com/v7/finance/quote?symbols=" + tickerYahoo);
            //    try
            //    {
            //        var responseObject = JObject.Parse(response.Content)["quoteResponse"]["result"];
            //        if (responseObject.HasValues)
            //        {
            //            company.YahooTicker = tickerYahoo;
            //            listUpdatedCompanys.Add(company);
            //            Console.WriteLine(tickerYahoo);
            //            continue;
            //        }

            //        if (companyBD[companyIndex].CountryId == 2)
            //        {
            //            var response2 = clientClass.GetAll("https://query1.finance.yahoo.com/v7/finance/quote?symbols=" + extraTicker);
            //            var ok = JObject.Parse(response2.Content)["quoteResponse"]["result"];


            //            if (ok.HasValues)
            //            {
            //                company.YahooTicker = extraTicker;
            //                listUpdatedCompanys.Add(company);
            //                Console.WriteLine(extraTicker);
            //                continue;
            //            }

            //            var response3 = clientClass.GetAll("https://query1.finance.yahoo.com/v7/finance/quote?symbols=" + canadaYahoo);
            //            var ok2 = JObject.Parse(response2.Content)["quoteResponse"]["result"];


            //            if (ok2.HasValues)
            //            {
            //                company.YahooTicker = canadaYahoo;
            //                listUpdatedCompanys.Add(company);
            //                Console.WriteLine(canadaYahoo);
            //            }



            //        }
            //    }
            //    catch (Exception)
            //    {

            //        Console.WriteLine("Error Here");
            //    }



            //}
            //genericDao.UpdateRange(listUpdatedCompanys);
        }




    }
}
