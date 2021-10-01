using KCSit.SalesforceAcademy.Lasagna.Business;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Controller.Controllers;
using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.DataAccess;
using KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace KCSit.SalesforceAcademy.Lasagna.AdminBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                        //.AllowAnyOrigin()
                        .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            services.AddResponseCaching();
            services.AddScoped<lasagnakcsContext>();
            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
                option.Password.RequiredLength = 8;
                option.Password.RequireLowercase = true;
                option.Password.RequireUppercase = true;
                option.Password.RequireDigit = true;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequiredUniqueChars = 5;
                option.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
            })
                .AddEntityFrameworkStores<lasagnakcsContext>()
                .AddTokenProvider("LasagnaApp", typeof(DataProtectorTokenProvider<ApplicationUser>)
                ).AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("BasicUserPolicy", policy => policy.RequireRole("BasicUser", "PremiumUser", "Manager", "Admin"));
                options.AddPolicy("PremiumUserPolicy", policy => policy.RequireRole("PremiumUser", "Manager", "Admin"));
                options.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Manager", "Admin"));
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            });

            // Cookie settings
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Unspecified;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.LoginPath = "/api/Admin/SignIn";
            });

            services.AddScoped<IAdminBO, AdminBO>();
            services.AddScoped<IGenericBusinessLogic, GenericBusinessLogic>();
            services.AddScoped<ISearchDAO, SearchDAO>();
            services.AddScoped<IGenericDAO, GenericDAO>();
            services.AddScoped<IPortfoliosDAO, PortfoliosDAO>();
            services.AddScoped<IGenericLogic, GenericLogic>();
            services.AddScoped<ICompaniesBO, CompaniesBO>();
            services.AddScoped<IPortfoliosBO, PortfoliosBO>();

            services.AddScoped<GenericBusinessLogic>();
            services.AddScoped<GenericController>();


            services.AddScoped<IRule1BO, Rule1BO>();
            services.AddScoped<IRule1DAO, Rule1DAO>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseResponseCaching();

            app.UseCookiePolicy(new CookiePolicyOptions {MinimumSameSitePolicy = SameSiteMode.None});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });


        }
    }
}

