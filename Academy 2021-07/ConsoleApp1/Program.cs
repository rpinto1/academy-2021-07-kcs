using KCSit.SalesforceAcademy.Kappify.Data;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!"); //changed code

            using(var context = new academykcsContext())
            {
                context.Artists.Add(new Artist
                {
                    Bio = "test",
                    Genre = "test",
                    ImageUrl = "test",
                    Name = "test",
               
                    Uuid = Guid.NewGuid()
                });
                context.SaveChanges();
            }
        }
    }
}
