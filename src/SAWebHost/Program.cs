using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SAWebHost.Data;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SAWebHost
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace;

        public static int Main(string[] args)
        {
            Console.Title = "Social Analytics Tokens Endpoint)";

            var configuration = GetConfiguration();
            var levelSwitch = new LoggingLevelSwitch(Serilog.Events.LogEventLevel.Information);
            Log.Logger = CreateSerilogLogger(configuration, levelSwitch);

            IHost host = default;
            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...", AppName);
                host = CreateHostBuilderWithOptions(configuration, levelSwitch, args)
                                    .Build();

                Log.Information("Applying migrations ({ApplicationContext})...", AppName);

                host.MigrateDbContext<ApplicationDbContext>((context, services) =>
                {
                });
                host.MigrateDbContext<SAWebHostContext>((context, services) =>
                {

                });



                Log.Information("Starting web host ({ApplicationContext})...", AppName);
                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
                return 1;
            }
            finally
            {
                host?.Dispose();
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// Required for EF Core Migrations using the EF Core Tools
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
            => CreateHostBuilderWithOptions(GetConfiguration(), new LoggingLevelSwitch(Serilog.Events.LogEventLevel.Verbose), args);


        private static IHostBuilder CreateHostBuilderWithOptions(IConfiguration configuration, LoggingLevelSwitch levelSwitch, string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                            .ConfigureServices(services =>
                            {
                                services.AddSingleton(levelSwitch);
                            })
                            .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                            .UseContentRoot(Directory.GetCurrentDirectory())
                            .ConfigureWebHostDefaults(webBuilder =>
                            {   
                                webBuilder.UseIIS();
                                webBuilder.UseStartup<Startup>()
                                    .ConfigureKestrel(options =>
                                    {
                                        options.AddServerHeader = false;
                                        options.ConfigureHttpsDefaults(opt =>
                                        {
                                        //opt.AllowAnyClientCertificate();
                                        //opt.ClientCertificateValidation = ValidateCert;
                                        opt.ClientCertificateMode = ClientCertificateMode.AllowCertificate;
                                        });
                                    });
                            })
                            .UseSerilog();
        }

        static bool ValidateCert(X509Certificate2 cert, X509Chain chain, SslPolicyErrors ssl)
            => true;

        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration, LoggingLevelSwitch levelSwitch)
        {
            return new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .Enrich.WithProperty("ApplicationContext", AppName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

    }
}
