using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DemoProject
{
    /// <summary>
    /// </summary>
    public class Program
    {
        /// <summary>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory()) //<--NOTE THIS
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        /// <summary>
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();
    }
}