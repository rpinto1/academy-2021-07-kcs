using KCSit.SalesforceAcademy.Kappify.DataAccess;


namespace Rui
{
    class Program
    {
        static void Main(string[] args)
        {
            var genericDao = new GenericDAO();


            var industries = new Industries();

            //industries.insertIndustries(genericDao);
            var companies = new CompaniesCode();

            companies.insertCompanies(genericDao);





        }
    }
}
