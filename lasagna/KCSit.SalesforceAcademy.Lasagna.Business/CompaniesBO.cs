using System;
using System.Collections.Generic;
using KCSit.SalesforceAcademy.Lasagna.Data;
using System.Threading.Tasks;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using System.Linq;

namespace KCSit.SalesforceAcademy.Lasagna.Business
{
    public class CompaniesBO : ICompaniesBO
    {

        private ISearchDAO _searchDAO;
        private IGenericDAO _genericDAO;
        private IGenericBusinessLogic _genericBusiness;
        private IRule1DAO _rule1DAO;


        public CompaniesBO(ISearchDAO searchDao, IGenericDAO genericDao, IGenericBusinessLogic genericBusiness, IRule1DAO rule1DAO)
        {
            _rule1DAO = rule1DAO;
            _searchDAO = searchDao;
            _genericDAO = genericDao;
            _genericBusiness = genericBusiness;
        }


        public async Task<GenericReturn<List<CompanyPoco>>> GetCompaniesNamesTickers(string companiesNamesTickers, int pageNumber)
        {
            var pageSize = 30;
            return await _genericBusiness.GenericTransaction(

            async () =>
            {

                return await _searchDAO.SearchCompaniesBySearchQuery(companiesNamesTickers, pageSize, pageNumber);
            }

            );
        }
        public async Task<GenericReturn<List<Industry>>> GetIndustries(string sectorName)
        {

            return await _genericBusiness.GenericTransaction(

            async () =>
            {

                return await _searchDAO.SearchIndustiesBySector(sectorName);
            }

            );
        }

        public async Task<GenericReturn<CompanyScorePoco>> GetCompanyByIIS(string sectorName, string indexName, string industryName, int page, List<string> countries)
        {


            return await _genericBusiness.GenericTransaction(

            async () =>
            {

                return await _searchDAO.SearchCompaniesByIndex(indexName, sectorName, industryName, page, countries);
            }

            );
        }

        public async Task<GenericReturn<GainLoseDBPoco>> GetTopGainerOrLoser(List<string> countries)
        {

            return await _genericBusiness.GenericTransaction(

            async () =>
            {
                var gainersResponse = await _searchDAO.SearchCompaniesPrice(false, countries);
                var losersResponse = await _searchDAO.SearchCompaniesPrice(true, countries);

                var gainLose = new GainLoseDBPoco { Gainers = gainersResponse, Losers = losersResponse };

                return gainLose;
            }

            );
        }



        public async Task<GenericReturn<CompanyScorePoco>> GetCompanyByIISAuthenticated(string sectorName, string indexName, string industryName, int page, List<string> countries)
        {

            return await _genericBusiness.GenericTransaction(

            async () => { return await _searchDAO.SearchCompaniesByIndexAuthenticated(indexName, sectorName, industryName, page, countries); }

            );

        }


        public async Task<GenericReturn<List<KeyRatioAndIncomeStatementValuesPoco>>> GetCompaniesIncomeStatement(string ticker)
        {

            return await _genericBusiness.GenericTransaction(

            async () =>
            {

            var keyRatiosAndIncomeStatementList = await _searchDAO.SearchCompaniesIncomeStatements(ticker);

            var revenue = (from krIncStat in keyRatiosAndIncomeStatementList
                           select krIncStat.Revenue).ToList();

            var revenueGrowth = (from krIncStat in keyRatiosAndIncomeStatementList
                                 select krIncStat.RevenueGrowth).ToList();

            var grossProfit = (from krIncStat in keyRatiosAndIncomeStatementList
                               select krIncStat.GrossProfit).ToList();

            var grossMargin = (from krIncStat in keyRatiosAndIncomeStatementList
                               select krIncStat.GrossMargin).ToList();

            var operatingProfit = (from krIncStat in keyRatiosAndIncomeStatementList
                               select krIncStat.OperatingProfit).ToList();

            var operatingMargin = (from krIncStat in keyRatiosAndIncomeStatementList
                               select krIncStat.OperatingMargin).ToList();

            var earningsPerShare = (from krIncStat in keyRatiosAndIncomeStatementList
                                    select krIncStat.DividendsPerShare).ToList();

            var EPSGrowth = (from krIncStat in keyRatiosAndIncomeStatementList
                             select krIncStat.EPSGrowth).ToList();

                var dividendPerShare = (from krIncStat in keyRatiosAndIncomeStatementList
                               select krIncStat.DividendsPerShare).ToList();

            var returnOnAssets = (from krIncStat in keyRatiosAndIncomeStatementList
                               select krIncStat.ReturnOnAssets).ToList();

            var returnOnEquity = (from krIncStat in keyRatiosAndIncomeStatementList
                               select krIncStat.ReturnOnEquity).ToList();

            var returnOnInvestedCapital = (from krIncStat in keyRatiosAndIncomeStatementList
                               select krIncStat.ReturnOnInvestedCapital).ToList();

            var list = new List<KeyRatioAndIncomeStatementValuesPoco>();


                for (int i = 0; i < keyRatiosAndIncomeStatementList.Count(); i++)
                {
                    list.Add(new KeyRatioAndIncomeStatementValuesPoco
                    {
                        Year = keyRatiosAndIncomeStatementList.ElementAt(i).Year,
                        Revenue = revenue.ElementAt(i),
                        RevenueGrowth = revenueGrowth.ElementAt(i),
                        GrossProfit = grossProfit.ElementAt(i),
                        GrossMargin = grossMargin.ElementAt(i),
                        OperatingProfit = operatingProfit.ElementAt(i),
                        OperatingMargin = operatingMargin.ElementAt(i),
                        EarningsPerShare = earningsPerShare.ElementAt(i),
                        EPSGrowth = EPSGrowth.ElementAt(i),
                        DividendsPerShare = dividendPerShare.ElementAt(i),
                        ReturnOnAssets = returnOnAssets.ElementAt(i),
                        ReturnOnEquity = returnOnEquity.ElementAt(i),
                        ReturnOnInvestedCapital = returnOnInvestedCapital.ElementAt(i)
                    });
                }

                return await Task.FromResult(list);

            });

        }
    }
}
