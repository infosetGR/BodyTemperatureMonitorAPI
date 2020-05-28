using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using TemperatureMonitorAPI.Data;


namespace TemperatureMonitorAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().CreateDatabase<TMContext>().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
               .UseStartup<Startup>();
               
    }
}
