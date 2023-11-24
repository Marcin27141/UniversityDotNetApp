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
using Microsoft.AspNetCore.Identity.UI.Services;
using Scrutor;
using System;
using WebApplication1.Services;
using WebApplication1.Database;
using WebApplication1.ApiServices.GenericRepositories.Students;
using WebApplication1.Contracts;
using WebApplication1.LocalServices;
using Microsoft.AspNetCore.Identity;
using WebApplication1.GraphQLServices.QueryGenerators;
using WebApplication1.Extensions;
using WebApplication1.GrpcServices;
using Microsoft.AspNetCore.Http;

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
            var connectionString = Configuration.GetConnectionString("DefaultWebDbConnection");
            services.AddDbContext<WebAppDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<WebAppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<WebAppDbContext>()
            .AddDefaultTokenProviders();

            services.AddRazorPages();

            services.AddAutoMapper(typeof(AutomapperConfiguration));
            services.AddAuthorizationServices();
            services.AddUsersServices();

            services.AddScoped<IGradesRepository, GradesRepository>();
            
            //either or
            services.AddRestApiServices();
            //services.AddGraphQLServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //SeedUserAccounts(app);

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

            app.UseStatusCodePagesWithRedirects("/errors/{0}");

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

        private static void SeedUserAccounts(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<WebAppUser>>();
                new UserGenerator(userManager).SeedUsers(50);
            }
        }
    }
}
