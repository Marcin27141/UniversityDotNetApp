using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1.DataBase;
using WebApplication1.DataBase.Entities;
using WebApplication1.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Policies.Handlers;


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
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbContext>();

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

            services.Scan(scan => scan
                .FromAssemblyOf<Startup>()
                .AddClasses(classes => classes.Where(c => c.Namespace.Contains("Services") && !c.Namespace.EndsWith("People")))
                .AsMatchingInterface()
                .WithScopedLifetime());
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
