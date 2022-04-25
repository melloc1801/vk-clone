using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace Vk_clone.Errors.Request
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.Setup().RegisterNLogWeb().GetCurrentClassLogger();
            logger.Debug("Init main");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().ConfigureLogging(logging =>
                        {
                            logging.ClearProviders();
                            logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                        })
                        .UseNLog();
                });
    }
}