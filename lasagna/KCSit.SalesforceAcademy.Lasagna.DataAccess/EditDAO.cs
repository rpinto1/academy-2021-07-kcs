using KCSit.SalesforceAcademy.Lasagna.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace KCSit.SalesforceAcademy.Lasagna.DataAccess
{
    class EditDAO
    {
        public void UpdatePortfolioId(Guid Uuid,List<string> Tickers,String PortfolioName)
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
}
}
