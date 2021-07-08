using KCSit.SalesforceAcademy.Kappify.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Kappify.DataAccess
{
    public class GenericDAO
    {
        public T Add<T>(T generic) where T : class
        {
            using (var context = new academykcsContext())
            {
                context.Set<T>().Add(generic);

                context.SaveChanges();

                return generic;
            }
        }

        public void AddRange<T>(List<T> generic) where T : class
        {
            using (var context = new academykcsContext())
            {
                context.Set<T>().AddRange(generic);

                context.SaveChanges();
            }
        }


        public void Update<T>(T generic) where T : class
        {
            using (var context = new academykcsContext())
            {
                context.Set<T>().Update(generic);

                context.SaveChanges();
            }
        }

        public void Delete<T>(T generic) where T : class
        {
            using (var context = new academykcsContext())
            {
                context.Set<T>().Remove(generic);

                context.SaveChanges();
            }
        }
        public void DeleteRange<T>(List<T> generic) where T : class
        {
            using (var context = new academykcsContext())
            {
                context.Set<T>().RemoveRange(generic);

                context.SaveChanges();
            }
        }

        public T Get<T>(Guid guid) where T : class
        {
            using (var context = new academykcsContext())
            {
                return context.Set<T>().Find(guid);

            }
        }
    }


}
