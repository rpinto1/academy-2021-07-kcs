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


            }
        }


        public async Task<PortfolioPoco> GetPortfolioWithoutCompanies(Guid portfolioUuid)
        {
            using (var context = new lasagnakcsContext())
            {
                var companies = await GetCompaniesByPortfolio(portfolioUuid);

                var query = await (from portfolio in context.Portfolios

                                   where portfolio.Uuid == portfolioUuid

                                   select new PortfolioPoco
                                   {
                                       PortfolioId = portfolio.Uuid,
                                       PortfolioName = portfolio.Name,

                                   })
                                       .SingleOrDefaultAsync();

                query.PortfolioCompanies = companies;

                return query;


            }


        }

        public async Task DeletePortfolioId(Guid Uuid)
        {
            using (var context = new lasagnakcsContext())
            {
                var remove = await (from portfolio in context.Portfolios
                              join portfolioCompany in context.PortfolioCompanies
                              on portfolio.Id equals portfolioCompany.PortfolioId

                              where portfolio.Uuid == Uuid

                              select portfolioCompany
                              ).ToListAsync();

                if (remove != null)
                {
                    foreach (PortfolioCompany portfolioCompany in remove)
                    { context.PortfolioCompanies.Remove(portfolioCompany); }

                }

                var removep = await (from portfolio in context.Portfolios

                               where portfolio.Uuid == Uuid

                               select portfolio
                              ).FirstOrDefaultAsync();
                if (removep != null)
                {
                    context.Portfolios.Remove(removep);
                }
                context.SaveChanges();
            }
        }

        public async Task UpdatePortfolioId( Guid Uuid, List<string> Tickers, String PortfolioName)
        {
            using (var context = new lasagnakcsContext())
            {
                var remove = await (from portfolio in context.Portfolios
                              join portfolioCompany in context.PortfolioCompanies
                              on portfolio.Id equals portfolioCompany.PortfolioId
                              join companies in (context.Companies.Where(c => Tickers.Contains(c.Ticker)).AsEnumerable())
                              on portfolioCompany.CompanyId equals companies.Id
                              where portfolio.Uuid == Uuid
                              select portfolioCompany
                              ).ToListAsync();

                if (remove != null)
                {
                    foreach (PortfolioCompany portfolioCompany in remove)
                    { context.PortfolioCompanies.Remove(portfolioCompany); }

                }


                var update = await (from portfolio in context.Portfolios

                              where portfolio.Uuid == Uuid

                              select portfolio
                              ).FirstOrDefaultAsync();
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

