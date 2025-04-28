using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TalkNest.Application.Exceptions;
using Newtonsoft.Json;


namespace TalkNest.Infrastructure.ErrorHandling
{
    public static class CustomErrorHandlingMiddleware
    {
        public static IServiceCollection AddCustomErrorHandlingMiddleware(this IServiceCollection services)
        {
            services.AddProblemDetails(x =>
            {
                // checks environment to control when an exception should be included (Development)
                x.IncludeExceptionDetails = (ctx, _) =>
                {
                    var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                    return env.IsDevelopment() || env.IsStaging();
                };
                x.ShouldLogUnhandledException = (context, exception, problemDetails) =>
                {
                    if (exception is ValidationException)
                    {
                        // Skip logging for specific exception types
                        return false;
                    }
                    // Log all other exceptions
                    return true;
                };
                
                
                // Exception will produce and returns from our FluentValidation RequestValidationBehavior
                x.Map<ValidationException>(ex => new ProblemDetails
                {
                    Title = "input validation rules broken",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = JsonConvert.SerializeObject(ex.ValidationResultModel.Errors),
                    Type = "https://somedomain/input-validation-rules-error"
                });
                x.Map<BadRequestException>(ex => new ProblemDetails
                {
                    Title = "bad request exception",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = ex.Message,
                    Type = "https://somedomain/bad-request-error"
                });
             
                x.Map<NotFoundException>(ex => new ProblemDetails
                {
                    Title = "not found exception",
                    Status = StatusCodes.Status404NotFound,
                    Detail = ex.Message,
                    Type = "https://somedomain/not-found-error"
                });
                x.Map<InternalServerException>(ex => new ProblemDetails
                {
                    Title = "api server exception",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = ex.Message,
                    Type = "https://somedomain/api-server-error"
                });

            });
            return services;
        }
    }
}
