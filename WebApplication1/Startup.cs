using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Policies.Handlers;
using WebApplication1.Configurations;
using UniversityApi.API.Contracts;
using WebApplication1.ApiServices.GenericRepositories.Professors;
using WebApplication1.ApiServices.GenericRepositories.Courses;
using WebApplication1.ApiServices.GenericRepositories.Students;
using WebApplication1.Contracts;
using WebApplication1.ApiServices;
using WebApplication1.Services;
using WebApplication1.ApiServices.GenericRepositories;
using WebApplication1.Services.People;
using ApiDtoLibrary.Students;
using NuGet.Protocol.Core.Types;
using Scrutor;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddRazorPages();

            //policies
            services.AddScoped<IAuthorizationHandler, HasAdminRightsHandler>();
            services.AddScoped<IAuthorizationHandler, StudentEditorIsOwnerHandler>();
            services.AddScoped<IAuthorizationHandler, ProfessorEditorIsOwnerHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("HasAdminRights", policyBuilder =>
                    policyBuilder.RequireClaim("IsAdmin"));
                options.AddPolicy("CanEditStudent", policyBuilder =>
                    policyBuilder.AddRequirements(new CanEditStudentRequrement()));
                options.AddPolicy("CanEditProfessor", policyBuilder =>
                    policyBuilder.AddRequirements(new CanEditProfessorRequirement()));

            }
            );

            //custom services

            //services.Scan(scan => scan
            //    .FromAssemblyOf<Startup>()
            //    .AddClasses(classes => classes.Where(c => c.Namespace.Contains("Services") && !c.Namespace.EndsWith("People")))
            //    .AsMatchingInterface()
            //    .WithScopedLifetime());

            services.Scan(scan =>
            {
                scan.FromAssemblyOf<Startup>()
                    .AddClasses(classes => classes.Where(c => c.Namespace.Contains("ApiServices")))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            //services.AddScoped(typeof(IGenericGetRepository<>), typeof(GenericGetRepository<,>));
            //services.AddScoped(typeof(IGenericPostRepository<>), typeof(GenericPostRepository<,>));
            //services.AddScoped(typeof(IGenericPutRepository<>), typeof(GenericPutRepository<,>));
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //services.AddScoped(IGenericGetRepository<Student>, GenericGetRepository<Student,GetStudent>));
            //services.AddScoped(typeof(IGenericPostRepository<,>), typeof(GenericPostRepository<,>));
            //services.AddScoped(typeof(IGenericPutRepository<,>), typeof(GenericPutRepository<,>));
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            

            //services.AddScoped<IProfessorsRepository, ProfessorRepository>();
            //services.AddScoped<ICoursesRepository, CourseRepository>();
            //services.AddScoped<IStudentsRepository, StudentRepository>();
            //services.AddScoped<IPeopleRepository, PeopleRepository>();
            //services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(AutomapperConfiguration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
