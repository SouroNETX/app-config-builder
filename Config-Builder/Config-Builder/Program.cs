using System.IO;
using System.Text;
using Config_Builder.Services.AppConfig;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Config_Builder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilderWithCustomJson(args).Build().Run();
        }

        // ReSharper disable once UnusedMember.Global
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }

        // Build IHostBuilder with Custom Configuration
        public static IHostBuilder CreateHostBuilderWithCustomJson(string[] args)
        {
            var appConfig = GetAppConfig.JsonMerger();

            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((_, config) =>
                {
                    config.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appConfig)));
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}