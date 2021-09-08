using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.DataAccess
{
    public class GenericDAO : IGenericDAO
    {
        public T Add<T>(T generic) where T : class
        {
            using (var context = new lasagnakcsContext())
            {
                try
                {
                    context.Set<T>().Add(generic);

                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                catch (Exception e)
                {
                    return null;
                }


                return generic;
            }
        }

        public async Task<T> AddAsync<T>(T generic) where T : class
        {
            using (var context = new lasagnakcsContext())
            {
                context.Set<T>().Add(generic);

                await context.SaveChangesAsync();

                return generic;
            }
        }

        public void AddRange<T>(List<T> generic) where T : class
        {
            using (var context = new lasagnakcsContext())
            {
                context.Set<T>().AddRange(generic);

                context.SaveChanges();
            }
        }


        public async Task AddRangeAsync<T>(List<T> generic) where T : class
        {
            using (var context = new lasagnakcsContext())
            {
                context.Set<T>().AddRange(generic);

                await context.SaveChangesAsync();
            }
        }


        public void Update<T>(T generic) where T : class
        {
            using (var context = new lasagnakcsContext())
            {
                context.Set<T>().Update(generic);

                context.SaveChanges();
            }
        }

        public async Task UpdateAsync<T>(T generic) where T : class
        {
            using (var context = new lasagnakcsContext())
            {
                context.Set<T>().Update(generic);

                await context.SaveChangesAsync();
            }
        }

        public void UpdateRange<T>(List<T> generic) where T : class
        {
            using (var context = new lasagnakcsContext())
            {
                context.Set<T>().UpdateRange(generic);

                context.SaveChanges();
            }
        }
        public async Task UpdateRangeAsync<T>(List<T> generic) where T : class
        {
            using (var context = new lasagnakcsContext())
            {
                context.Set<T>().UpdateRange(generic);

                await context.SaveChangesAsync();
            }
        }

        public void Delete<T>(T generic) where T : class
        {
            using (var context = new lasagnakcsContext())
            {
                context.Set<T>().Remove(generic);

                context.SaveChanges();
            }
        }

        public async Task DeleteAsync<T>(T generic) where T : class
        {
            using (var context = new lasagnakcsContext())
            {
                context.Set<T>().Remove(generic);

                await context.SaveChangesAsync();
            }
        }

        public void DeleteRange<T>(List<T> generic) where T : class
        {
            using (var context = new lasagnakcsContext())
            {
                context.Set<T>().RemoveRange(generic);

                context.SaveChanges();
            }
        }

        public async Task DeleteRangeAsync<T>(List<T> generic) where T : class
        {
            using (var context = new lasagnakcsContext())
            {
                context.Set<T>().RemoveRange(generic);

                await context.SaveChangesAsync();
            }
        }

        public T Get<T>(int id) where T : class
        {
            using (var context = new lasagnakcsContext())
            {
                return context.Set<T>().Find(id);

            }
        }

        /// <summary> nao devia testar aqui
        /// Get Subindystry or Industry
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>


        public T Get<T>(Guid guid) where T : class, IEntity
        {
            using (var context = new lasagnakcsContext())
            {
                return context.Set<T>().Where(item => item.Uuid == guid).SingleOrDefault();

            }
        }


        public async Task<T> GetAsync<T>(Guid guid) where T : class, IEntity
        {
            using (var context = new lasagnakcsContext())
            {
                return await context.Set<T>().Where(item => item.Uuid == guid).SingleOrDefaultAsync();

            }
        }

        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            using (var context = new lasagnakcsContext())
            {
                return await context.Set<T>().ToListAsync();

            }
        }

        public List<T> GetAll<T>() where T : class
        {
            using (var context = new lasagnakcsContext())
            {
                return context.Set<T>().ToList();

            }
        }
    }


}
