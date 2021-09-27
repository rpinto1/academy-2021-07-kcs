using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.DataAccess
{
    public class AdminDAO : IAdminDAO
    {

        public async Task<IEnumerable> GetUsers()
        {
            using (var context = new lasagnakcsContext())
            {
                //var users = (from user in context.Users
                //             join claims in context.UserClaims
                //             on user.Id equals claims.UserId
                //             where user.FirstName.ToLower().Contains(filter.firstName.ToLower()) &&
                //                   user.LastName.ToLower().Contains(filter.lastName.ToLower()) &&
                //                   user.Email.ToLower().Contains(filter.email.ToLower()) &&
                //                   claims.ClaimValue.Contains(filter.role)

                //             select new UserPoco
                //             {
                //                 Id = user.Id,
                //                 FirstName = user.FirstName,
                //                 LastName = user.LastName,
                //                 Email = user.Email,
                //                 Role = claims.ClaimValue,
                //             })

                //             //.OrderBy(orderByField, orderByDirection)
                //             //.Skip(skip)
                //             //.Take(take)
                //             .ToList();

                //return Task.FromResult(new UserPocoList { Users = users, Total = users.Count() });
                return null;
            }

        }
    }
}
