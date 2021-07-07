using KCSit.SalesforceAcademy.Kappify.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class App
    {
        public void Run()
        {
            Console.WriteLine("Hello World!"); //changed code

            //var generic = new GenericDAO();
            //generic.Add<Artist>(new Artist
            //{
            //    Bio = "Joana",
            //    Genre = "Joana",
            //    Name = "Joana",
            //    Uuid = Guid.NewGuid(),
            //    ImageUrl = "Joana"

            //} );

            var dao = new SearchDAO();
            var results = dao.SearchSongByName("nothing");
            var songByLabel = dao.SearchSongsByLabel("KUAlq");

            foreach (var result in results)
                Print(result);

            foreach (var songByLabel1 in songByLabel)
                Print(songByLabel1);

            Console.ReadLine();
        }


        public void Print(object obj)
        {
            var props = obj.GetType().GetProperties();
            foreach(var prop in props)
            {
                var value = prop.GetValue(obj);
                Console.Write(prop.Name + " = ");
                Console.Write(value != null ? value : "null");
                Console.WriteLine();
            }
        }
    }
}
