using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            var configuringFileName = "nlog.config";

            //If we inspect the Hosting aspnet code, we'll see that it internally looks the 
            //ASPNETCORE_ENVIRONMENT environment variable to determine the actual environment
            var aspnetEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var environmentSpecificLogFileName = $"nlog.{aspnetEnvironment}.config";

            if (File.Exists(environmentSpecificLogFileName))
            {
                configuringFileName = environmentSpecificLogFileName;
            }

            // NLog: setup the logger first to catch all errors
            var logger = NLogBuilder.ConfigureNLog(configuringFileName).GetCurrentClassLogger();

            try
            {
                logger.Debug("init program");
                CreateHostBuilder(args).Build().Run();

            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
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

                 .UseNLog(); // NLog: Setup NLog for Dependency injection
    }
#pragma warning restore CS1591
}
