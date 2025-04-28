using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TalkNest.Api.Web
{
    public static class ApiVersioningExtensions
    {
        public static void AddCustomVersioning(this IServiceCollection services,
            Action<ApiVersioningOptions> configurator = null)
        {

            services.AddApiVersioning(options =>
               {
                   // Add the headers "api-supported-versions" and "api-deprecated-versions"
                   options.ReportApiVersions = true;
                   options.AssumeDefaultVersionWhenUnspecified = true;
                   options.DefaultApiVersion = new ApiVersion(1, 0);
                   configurator?.Invoke(options);
               });
        }
    }

}
