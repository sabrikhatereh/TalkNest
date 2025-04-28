using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Serilog;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using TalkNest.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TalkNest.Api.Serilog;

namespace TalkNest
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            Log.Logger = host.RegisterSerilog();

            await UpdateDatabase(host);
            await host.RunAsync();
        }
        private static async Task UpdateDatabase(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogWarning(environment);
                var dbContext = scope.ServiceProvider.GetRequiredService<TalkNestWriteDbContext>();
                var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
                try
                {
                    if (pendingMigrations.Any())
                    {
                        await dbContext.Database.MigrateAsync();
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database.");
                    throw;
                }
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var webHost = WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables();
                })
                .UseEnvironment(environment)
                .UseStartup<Startup>()

                .ConfigureKestrel(serverOptions =>
                {
                    serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(5);
                });

            webHost.UseSerilog();
            return webHost;
        }

    }
}