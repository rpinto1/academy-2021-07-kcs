using KCSit.SalesforceAcademy.Lasagna.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace KCSit.SalesforceAcademy.Lasagna.DailyUpdates
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>

            Host.CreateDefaultBuilder(args)

                    .ConfigureServices(services =>
                    {
                        services.AddHostedService<WindowsBackgroundService>();
                        services.AddSingleton<UpdatePrice>();
                    })
                    ;
    }
}
