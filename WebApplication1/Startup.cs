using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1.DataBase;
using WebApplication1.DataBase.Entities;
using WebApplication1.Services.CourseOps;
using WebApplication1.Services.PeopleOps;
using WebApplication1.Services.ProfessorOps;
using WebApplication1.Services.StudentOps;
using WebApplication1.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Policies.Handlers;
using WebApplication1.Services.UserOps;

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

            services.AddScoped<ICreateCourseOp, CreateCourseOp>();
            services.AddScoped<IDeleteCourseOp, DeleteCourseOp>();
            services.AddScoped<IReadCourseOp, ReadCourseOp>();
            services.AddScoped<IUpdateCourseOp, UpdateCourseOp>();

            services.AddScoped<ICreateProfessorOp, CreateProfessorOp>();
            services.AddScoped<IDeleteProfessorOp, DeleteProfessorOp>();
            services.AddScoped<IReadProfessorOp, ReadProfessorOp>();
            services.AddScoped<IUpdateProfessorOp, UpdateProfessorOp>();

            services.AddScoped<ICreateStudentOp, CreateStudentOp>();
            services.AddScoped<IDeleteStudentOp, DeleteStudentOp>();
            services.AddScoped<IReadStudentOp, ReadStudentOp>();
            services.AddScoped<IUpdateStudentOp, UpdateStudentOp>();

            services.AddScoped<IReadPeopleOp, ReadPeopleOp>();
            services.AddScoped<IReadUserOp, ReadUserOp>();


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
