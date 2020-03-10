using System;
using LogWire.Controller.Client.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace LogWire.Agent.Ingress
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    string endpoint = Environment.GetEnvironmentVariable("lw_controller_endpoint");
                    string token = Environment.GetEnvironmentVariable("lw_access_token");

                    webBuilder.ConfigureAppConfiguration(builder =>
                    {
                        builder.AddEnvironmentVariables("lw_");
                        builder.AddControllerConfiguration(endpoint, "ingress.agent", token);
                        builder.AddControllerConfiguration(endpoint, "rabbitmq", token);
                    });

                    webBuilder.UseUrls("https://0.0.0.0:5004");

                    webBuilder.UseStartup<Startup>();

                });
    }
}
