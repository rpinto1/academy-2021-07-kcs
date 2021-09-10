using KCSit.SalesforceAcademy.Lasagna.Business;
using KCSit.SalesforceAcademy.Lasagna.Controller.Controllers;
using KCSit.SalesforceAcademy.Lasagna.DataAccess;
using System;

namespace KCSit.SalesforceAcademy.Lasagna.Rule1
{
    class Program
    {
        static void Main(string[] args)
        {
            var genericDAO = new GenericDAO();
            var searchDAO = new SearchDAO();
            var genericBusinessLogic = new GenericBusinessLogic();
            var genericControllerReturn = new GenericControllerReturn();

            var rule1BO = new Rule1BO(genericDAO, searchDAO, genericBusinessLogic);

            var rule1Controller = new Rule1Controller(rule1BO, genericControllerReturn);




            //rule1Controller.UpdateScore("ACKBF:US");

            rule1Controller.UpdateAllScores();




        }
    }
}
