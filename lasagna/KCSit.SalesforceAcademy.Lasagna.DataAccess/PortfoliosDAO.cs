using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace KCSit.SalesforceAcademy.Lasagna.DataAccess
{
    public class PortfoliosDAO : IPortfoliosDAO
    {

        public async Task<List<PortfolioPoco>> GetPortfolios(Guid userId)
        {
            using (var context = new lasagnakcsContext())
            {

                return await (from portfolio in context.Portfolios
                              where portfolio.UserId == userId.ToString()

                              select new PortfolioPoco
                              {
                                  PortfolioId = portfolio.Uuid,
                                  PortfolioName = portfolio.Name,

                              })

                        .ToListAsync();
            }
        }

        public async Task<List<PortfolioCompanyPoco>> GetCompaniesByPortfolio(Guid portfolioUuid)
        {
            using (var context = new lasagnakcsContext())
            {
                return await (from portfolio in context.Portfolios
                              join portfolioCompany in context.PortfolioCompanies
                              on portfolio.Id equals portfolioCompany.PortfolioId
                              join company in context.Companies
                              on portfolioCompany.CompanyId equals company.Id

                              where portfolio.Uuid == portfolioUuid

                              select new PortfolioCompanyPoco
                              {
                                  Ticker = company.Ticker,
                                  Name = company.Name,

                              })

                        //.Take(20)
                        .ToListAsync();


                //var keyRatiosList = (await GetKeyRatios(ticker)).ToList();
                //var balanceSheetList = (await GetBalanceSheet(ticker)).ToList();
                //var incomeStatementList = (await GetIncomeStatement(ticker)).ToList();

                //var list = new List<PortfolioCompanyValuesPoco>();


                //for (int i = 0; i < keyRatiosList.Count; i++)
                //{
                //    list.Add(new PortfolioCompanyValuesPoco
                //    {
                //        Year = keyRatiosList[i].Year,
                //        ROIC = roic[i],
                //        Equity = equity[i],
                //        EPS = eps[i],
                //        Sales = sales[i],
                //        Cash = cash[i]
                //    });

                //for (int i = keyRatiosList.Count - 1; i > 0; i--)
                //{
                //    list.Add(new PortfolioCompanyValuesPoco
                //    {
                //        Year = keyRatiosList[i].Year,
                //        ROIC = keyRatiosList[i].Roic,
                //        Equity = balanceSheetList[i].Equity,
                //        EPS = incomeStatementList[i].Eps,
                //        Sales = incomeStatementList[i].Sales,
                //        Cash = balanceSheetList[i].Cash
                //    });

                //}

                //return await Task.FromResult(list);


                //////////////await (from company in context.Companies
                //////////////       join yearlyReport in context.YearlyReports
                //////////////       on company.Id equals yearlyReport.CompanyId

                //////////////       join keyRatios in context.KeyRatios
                //////////////       on yearlyReport.KeyRatioId equals keyRatios.Id
                //////////////       into keyRatiosTable

                //////////////       join balanceSheet in context.BalanceSheets
                //////////////       on yearlyReport.BalanceSheetId equals balanceSheet.Id
                //////////////       into balanceSheetTable

                //////////////       join incomeStatement in context.IncomeStatements
                //////////////       on yearlyReport.IncomeStatementId equals incomeStatement.Id
                //////////////       into incomeStatementTable

                //////////////       where company.Ticker.Equals(ticker)
                //////////////       orderby yearlyReport.Year ascending

                //////////////       select new //KeyRatiosPoco
                //////////////       {
                //////////////           Ticker = company.Ticker,
                //////////////           Year = (int?)yearlyReport.Year ?? 0,
                //////////////           Roic = (decimal?)keyRatios.ReturnOnInvestedCapital ?? 0,
                //////////////           Equity = (decimal?)balanceSheet.ShareholdersEquity ?? 0,
                //////////////           Cash = (decimal?)balanceSheet.CashAndEquivalents ?? 0,
                //////////////           Eps = (decimal?)incomeStatement.Epsbasic ?? 0,
                //////////////           Sales = (decimal?)incomeStatement.Revenue ?? 0
                //////////////       })

                //////////////       .Take(20)
                //////////////       .ToListAsync();





            }
        }




        public void DeletePortfolioId(Guid Uuid)
        {
            using (var context = new lasagnakcsContext())
            {
                var remove = (from portfolio in context.Portfolios
                              join portfolioCompany in context.PortfolioCompanies
                              on portfolio.Id equals portfolioCompany.PortfolioId

                              where portfolio.Uuid == Uuid

                              select portfolioCompany
                              ).ToList();

                if (remove != null)
                {
                    foreach (PortfolioCompany portfolioCompany in remove)
                    { context.PortfolioCompanies.Remove(portfolioCompany); }

                }

                var removep = (from portfolio in context.Portfolios

                               where portfolio.Uuid == Uuid

                               select portfolio
                              ).FirstOrDefault();
                if (removep != null)
                {
                    context.Portfolios.Remove(removep);
                }
                context.SaveChanges();
            }
        }

        public void UpdatePortfolioId(Guid Uuid, List<string> Tickers, String PortfolioName)
        {
            using (var context = new lasagnakcsContext())
            {
                var remove = (from portfolio in context.Portfolios
                              join portfolioCompany in context.PortfolioCompanies
                              on portfolio.Id equals portfolioCompany.PortfolioId
                              join companies in (context.Companies.Where(c => Tickers.Contains(c.Ticker)).AsEnumerable())
                              on portfolioCompany.CompanyId equals companies.Id
                              where portfolio.Uuid == Uuid
                              select portfolioCompany
                              ).ToList();

                if (remove != null)
                {
                    foreach (PortfolioCompany portfolioCompany in remove)
                    { context.PortfolioCompanies.Remove(portfolioCompany); }

                }


                var update = (from portfolio in context.Portfolios

                              where portfolio.Uuid == Uuid

                              select portfolio
                              ).FirstOrDefault();
                if (update != null)
                {
                    update.Name = PortfolioName;
                    context.Portfolios.Update(update);
                }
                context.SaveChanges();
            }
        }




        public async Task<int> GetPortfolioId(Guid portfolioUuid)
        {
            using (var context = new lasagnakcsContext())
            {

                var activePortfolio = await (
                    from portfolio in context.Portfolios
                    where portfolio.Uuid == portfolioUuid

                    select new Portfolio())
                    .ToListAsync();

                return activePortfolio.GetEnumerator().Current.Id;

            }
        }

    }

}

