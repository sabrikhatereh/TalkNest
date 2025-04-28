using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TalkNest.Application;
using TalkNest.Application.Abstractions.DbContexts;
using TalkNest.Application.Behaviors;
using TalkNest.Core;
using TalkNest.Core.Abstractions;
using TalkNest.Core.Abstractions.Services;
using TalkNest.Infrastructure.Persistence;
using TalkNest.Infrastructure.Persistence.DbContexts;
using TalkNest.Infrastructure.Repositories.CommandRepositories;
using TalkNest.Infrastructure.Repositories.QueryRepositories;
using TalkNest.Infrastructure.Services;
using System.Reflection;
using TalkNest.Core.Abstractions.Events;
using TalkNest.Application.Events;
using System;

namespace TalkNest.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Add write DbContext
            services.AddDbContext<TalkNestWriteDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            //options.UseSqlite("Data Source=app.db"));

            // Register interfaces for read and write contexts
            services.AddScoped<IApplicationReadDb, ApplicationReadDb>();
            services.AddScoped<IApplicationWriteDbContext, TalkNestWriteDbContext>();
            services.TryAddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddEasyCaching(options => { options.UseInMemory(configuration, "mem"); });
           
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(Assembly.GetAssembly(typeof(ApplicationLayer))!);
                configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                configuration.AddRequestPreProcessor(typeof(RequestValidationBehavior<>));
            });
            services.RegisterRepositories();
            services.RegisterService();
        }
        private static void RegisterService(this IServiceCollection services)
        {
            services.AddSingleton<IForbidWords, ForbidWords>();
        }
        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPostCommandRepository, PostCommandRepository>();
            services.AddScoped<IPostQueryRepository, PostQueryRepository>();
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        }
    }
}
