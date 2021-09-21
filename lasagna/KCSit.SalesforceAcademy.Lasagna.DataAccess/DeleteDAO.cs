using KCSit.SalesforceAcademy.Lasagna.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace KCSit.SalesforceAcademy.Lasagna.DataAccess
{
    class DeleteDAO
    {
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
                    foreach(PortfolioCompany portfolioCompany in remove)
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
}
}
