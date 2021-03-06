using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Interfaces
{
    public interface ICompaniesBO
    {
        //string GetIndex();
        //string GetIndustries(string sectorName);
        //string GetSectors();
        Task<GenericReturn<List<Industry>>> GetIndustries(string sectorName);

        Task<GenericReturn<List<CompanyPoco>>> GetCompaniesNamesTickers(string companiesNamesTickers, int pageNumber);

        Task<GenericReturn<CompanyScorePoco>> GetCompanyByIIS(string sectorName, string indexName, string industryName, int page, List<string> countries);

        Task<GenericReturn<CompanyScorePoco>> GetCompanyByIISAuthenticated(string sectorName, string indexName, string industryName, int page, List<string> countries);

        Task<GenericReturn<GainLoseDBPoco>> GetTopGainerOrLoser(List<string> countries);

        Task<GenericReturn<List<KeyRatioAndIncomeStatementValuesPoco>>> GetCompaniesIncomeStatement(string ticker);
    }
}