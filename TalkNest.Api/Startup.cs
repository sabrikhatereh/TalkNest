using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TalkNest.Infrastructure.ErrorHandling;
using TalkNest.Infrastructure;
using TalkNest.Application;
using Hellang.Middleware.ProblemDetails;
using Microsoft.OpenApi.Models;

using FluentValidation;
using TalkNest.Application.Mapping;
using TalkNest.Core.Configuration;
using TalkNest.Api.SwaggerHelper;
using AutoMapper;
using Serilog;
using System;

namespace TalkNest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCustomErrorHandlingMiddleware();
            services.AddInfrastructure(Configuration);
            services.AddValidatorsFromAssemblyContaining<ApplicationLayer>();
            services.AddAutoMapper(typeof(MappingProfile));
            AddConfiguration(services);
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            services.AddHealthChecks();
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TalkNest APIs"
                });

                swagger.OperationFilter<RemoveVersionFromParameter>();
                swagger.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                swagger.DocInclusionPredicate((_, api) =>
                {
                    return true;
                });
            });
            
        }
        //
        private void AddConfiguration(IServiceCollection services)
        {
            services.Configure<ValidationSettings>(Configuration.GetSection("ValidationSettings"));
            services.Configure<LogOptions>(Configuration.GetSection("LogOptions"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            if (env.IsDevelopment() || environment == "Docker")
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenUp v1"); });
            }
            app.UseSerilogRequestLogging();
           
            app.UseProblemDetails();
            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}