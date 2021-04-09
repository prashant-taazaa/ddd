using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace todo.api
{
#pragma warning disable CS1591
    public class Program
    {
        public static void Main(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = new ConfigurationBuilder()
                             .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                             .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                             .Build();
            var nLogConfig = config.GetSection("NLog");
            LogManager.Configuration = new NLogLoggingConfiguration(nLogConfig);

            var logger = NLog.Web.NLogBuilder.ConfigureNLog(LogManager.Configuration).GetCurrentClassLogger();
            try
            {
                logger.Debug("Init main");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                //  .ConfigureAppConfiguration((hostingContext, config) =>
                //  {
                //var env = hostingContext.HostingEnvironment;

                //config
                //      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                //config.AddEnvironmentVariables(); // overwrites previous values

             
                //  })

                  .UseNLog(); 
    }
#pragma warning restore CS1591
}
