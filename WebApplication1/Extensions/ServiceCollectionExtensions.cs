using ApiDtoLibrary.Person;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System;
using WebApplication1.Contracts;
using WebApplication1.GraphQLServices;
using WebApplication1.GraphQLServices.QueryGenerators;
using WebApplication1.LocalServices;
using WebApplication1.Policies.Handlers;
using WebApplication1.Policies.Requirements;

namespace WebApplication1.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRestApiServices(this IServiceCollection services)
        {
            services.Scan(scan =>
            {
                scan.FromAssemblyOf<Startup>()
                    .AddClasses(classes => classes.Where(c => c.Namespace.Contains("ApiServices")))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            return services;
        }

        public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
        {
            services.Scan(scan =>
            {
                scan.FromAssemblyOf<Startup>()
                    .AddClasses(classes => classes.Where(c => c.Namespace.Contains("GraphQLServices") && c != typeof(GraphQLException)))
                    .UsingRegistrationStrategy(RegistrationStrategy.Append)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            return services;
        }

        public static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, StudentEditorIsOwnerHandler>();
            services.AddScoped<IAuthorizationHandler, ProfessorEditorIsOwnerHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("HasAdminRights", policyBuilder =>
                    policyBuilder.RequireClaim("IsAdmin"));
                options.AddPolicy("HasProfessorRights", policyBuilder =>
                    policyBuilder.RequireAssertion(
                        ctx => ctx.User.HasClaim(c => c.Type == "IsAdmin") || ctx.User.HasClaim("Status", Enum.GetName(PersonStatus.Professor))
                        ));
                options.AddPolicy("IsProfessor", policyBuilder =>
                    policyBuilder.RequireClaim("Status", Enum.GetName(PersonStatus.Professor)));
                options.AddPolicy("IsStudent", policyBuilder =>
                    policyBuilder.RequireClaim("Status", Enum.GetName(PersonStatus.Student)));
                options.AddPolicy("CanEditStudent", policyBuilder =>
                    policyBuilder.AddRequirements(new CanEditStudentRequrement()));
                options.AddPolicy("CanEditProfessor", policyBuilder =>
                    policyBuilder.AddRequirements(new CanEditProfessorRequirement()));
            }
            );

            return services;
        }

        public static IServiceCollection AddUsersServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationRepository, LocalAuthenticationRepository>();
            services.AddScoped<IUserRepository, LocalUserRepository>();

            return services;
        }        
    }
}
