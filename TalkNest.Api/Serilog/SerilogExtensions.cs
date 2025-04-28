using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Serilog.Events;
using Serilog;
using System;
using TalkNest.Core.Configuration;
using Serilog.Core;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Exceptions;
using Serilog.Sinks.SpectreConsole;

namespace TalkNest.Api.Serilog
{
    public static class SerilogExtensions
    {
       
        public static Logger RegisterSerilog(this IWebHost host)
        {
            var _serviceProvider = host.Services;
            var _configuration = _serviceProvider.GetRequiredService<IConfiguration>();
            var logOptions = _serviceProvider.GetRequiredService<IOptions<LogOptions>>().Value;
            var logLevel = Enum.TryParse<LogEventLevel>(logOptions.Level, true, out var level)
               ? level
               : LogEventLevel.Information;

            var logConfig = new LoggerConfiguration()
             .MinimumLevel.Is(logLevel)
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
               // Only show ef-core information in error level
               .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
               // Filter out ASP.NET Core infrastructure logs that are Information and below
               .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
               .Enrich.WithExceptionDetails()
               .Enrich.FromLogContext()
               .WriteTo.SpectreConsole(logOptions.LogTemplate, logLevel);

            return logConfig.ReadFrom.Configuration(_configuration).CreateLogger();
        }



    }

}
