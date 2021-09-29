using KCSit.SalesforceAcademy.Lasagna.Business.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Data.ViewModels;
using KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.DataAccess;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;


namespace KCSit.SalesforceAcademy.Lasagna.Business
{
    public class AdminBO : IAdminBO
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IGenericBusinessLogic _genericBusinessLogic;
        private readonly IPortfoliosDAO _portfoliosDAO;

        public AdminBO(IGenericBusinessLogic genericBusinessLogic, 
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IPortfoliosDAO portfoliosDAO)
        {
            this._genericBusinessLogic = genericBusinessLogic;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._portfoliosDAO = portfoliosDAO;

        }


        public async Task<GenericReturn<IdToken>> SignIn(SignInViewModel model)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                var userSharedFeatures = new UserSharedFeatures(_userManager, _signInManager);

                return await userSharedFeatures.SignIn(model);
            });
        }


        public async Task<GenericReturn> SignOut(string userId)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                var userSharedFeatures = new UserSharedFeatures(_userManager, _signInManager);

                userSharedFeatures.SignOut(userId);
            });
        }

        public async Task<GenericReturn<UserPoco>> CreateUser(AdminUpdateViewModel model)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                const string defaultPassword = "Test1234%";

                var user = new ApplicationUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                };

                var result = await _userManager.CreateAsync(user, defaultPassword);

                if (!result.Succeeded)
                {
                    string errorMsg = "";
                    foreach (var error in result.Errors)
                    {
                        errorMsg = String.Concat(errorMsg, error.Description, " ");
                    }
                    throw new Exception(errorMsg);
                }

                var claims = new List<Claim> { new Claim(ClaimTypes.Role, model.Role) };

                await _userManager.AddClaimsAsync(user, claims);

                return new UserPoco { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, Role = model.Role };
            });
        }


        public async Task<GenericReturn<UserPoco>> GetUser(string userId)
        {
            return await _genericBusinessLogic.GenericTransaction(() =>
            {
                if (String.IsNullOrEmpty(userId))
                {
                    throw new Exception("User not found.");
                }

                var user = _userManager.Users.Where(u => u.Id == userId).SingleOrDefault();

                if (user == null)
                {
                    throw new Exception("User does not exist");
                }

                var userPoco = new UserPoco
                {
                    Id = userId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = _userManager.GetClaimsAsync(user).Result.First().Value
                };

                return Task.FromResult(userPoco);
            });
        }


        public async Task<GenericReturn<UserPocoList>> GetUsers(string queryString)
        {
            return await _genericBusinessLogic.GenericTransaction(() =>
            {
                if (String.IsNullOrEmpty(queryString))
                {
                    queryString = "?filter=%7B%7D&range=%5B0%2C9%5D&sort=%5B%22id%22%2C%22ASC%22%5D";
                }


                // ?filter={}&range=[0,9]&sort=["id","ASC"]
                // "?filter={"firstName":"joana","email":"joana@lasagna.pt"}&range=[0,24]&sort=["id","ASC"]"
                var filterStr = HttpUtility.ParseQueryString(queryString).Get("filter");

                var filter = new AdminUserPoco();
                if (filterStr != null)
                {
                    filter = JsonSerializer.Deserialize<AdminUserPoco>(filterStr);
                    filter.firstName ??= "";
                    filter.lastName ??= "";
                    filter.email ??= "";
                    filter.role ??= "";
                }

                // [0, 9],  [10, 19],  [20, 25] ...
                var skip = 0;
                var take = 0;
                var rangeStr = HttpUtility.ParseQueryString(queryString).Get("range");
                if (rangeStr != null)
                {
                    var first = int.Parse(rangeStr.Substring(1, rangeStr.IndexOf(",") - rangeStr.IndexOf("[") - 1));
                    var last = int.Parse(rangeStr.Substring(rangeStr.IndexOf(",") + 1, rangeStr.IndexOf("]") - rangeStr.IndexOf(",") - 1));
                    skip = first;
                    take = last - first + 1;
                }

                // sort=["id","ASC"]"
                var orderByField = "";
                var orderByDirection = "Ascending";
                var sortStr = HttpUtility.ParseQueryString(queryString).Get("sort");
                if (sortStr != null)
                {
                    orderByField = sortStr.Split("\"")[1];
                    orderByField = orderByField.ToUpper().ElementAt(0) + orderByField.Substring(1);
                    orderByDirection = sortStr.Contains("ASC") ? "Ascending" : "Descending";
                }


                using (var context = new lasagnakcsContext())
                {
                    var users = (from user in context.Users
                                 join claims in context.UserClaims
                                 on user.Id equals claims.UserId
                                 where user.FirstName.ToLower().Contains(filter.firstName.ToLower()) &&
                                       user.LastName.ToLower().Contains(filter.lastName.ToLower()) &&
                                       user.Email.ToLower().Contains(filter.email.ToLower()) &&
                                       claims.ClaimValue.Contains(filter.role)

                                 select new UserPoco
                                 {
                                     Id = user.Id,
                                     FirstName = user.FirstName,
                                     LastName = user.LastName,
                                     Email = user.Email,
                                     Role = claims.ClaimValue,
                                 })

                                 .OrderBy(orderByField, orderByDirection)
                                 .Skip(skip)
                                 .Take(take)
                                 .ToList();

                    return Task.FromResult(new UserPocoList { Users = users, Total = users.Count() });
                }

            });
        }


        public async Task<GenericReturn> UpdateUser(string userId, AdminUpdateViewModel newModel)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    throw new Exception("User does not exist");
                }

                if (user.Email != newModel.Email)
                {
                    throw new Exception("User can not change email address");
                }

                // user data is ok. Update user
                user.FirstName = newModel.FirstName;
                user.LastName = newModel.LastName;

                var claims = await _userManager.GetClaimsAsync(user);
                await _userManager.RemoveClaimsAsync(user, claims);
                var newClaims = new List<Claim> { new Claim(ClaimTypes.Role, newModel.Role) };
                await _userManager.AddClaimsAsync(user, newClaims);

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description.ToString());
                }
                return userId;
            });
        }


        public async Task<GenericReturn> DeleteUser(Guid userId)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                {
                    throw new Exception("User does not exist");
                }

                var portfolios = await _portfoliosDAO.GetPortfolios(userId);

                foreach (PortfolioPoco portfolio in portfolios)
                {
                    await _portfoliosDAO.DeletePortfolioId(portfolio.PortfolioId);
                }

                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    string errorMsg = "";
                    foreach (var error in result.Errors)
                    {
                        errorMsg = String.Concat(errorMsg, error.Description, " ");
                    }
                    throw new Exception(errorMsg);
                }

                return new UserPoco { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email };

            });
        }


        public async Task<GenericReturn> DeleteUsers(string queryString)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                var idStr = queryString.Substring(7, queryString.Length - 9).Split(",");

                var deletedUsers = new List<Guid>();

                foreach (string id in idStr)
                {
                    var uuid = Guid.Parse(JsonSerializer.Deserialize<string>(id));

                    var result = await DeleteUser(uuid);

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Message);
                    }

                    deletedUsers.Add(uuid);
                }

                return deletedUsers;
            });
        }




        public async Task<GenericReturn> AddClaim(string userId, Claim claim)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {

                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    throw new Exception("User does not exist");
                }

                await _userManager.AddClaimAsync(user, claim);

            });
        }

        public async Task<GenericReturn> RemoveClaim(string userId, Claim claim)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {

                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    throw new Exception("User does not exist");
                }

                await _userManager.RemoveClaimAsync(user, claim);

            });
        }

        public async Task<GenericReturn<IList<Claim>>> GetClaims(string userId)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {

                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    throw new Exception("User does not exist");
                }

                var claims = await _userManager.GetClaimsAsync(user);

                return claims;

            });
        }




    }
}
