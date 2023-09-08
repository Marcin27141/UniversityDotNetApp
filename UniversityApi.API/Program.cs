using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Text;
using UniversityApi.API.Configurations;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase;
using UniversityApi.API.GraphQL;
using UniversityApi.API.GraphQL.Courses;
using UniversityApi.API.GraphQL.Professors;
using UniversityApi.API.GraphQL.Students;
using UniversityApi.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("UniversityDbConnectionString");
builder.Services.AddDbContext<UniversityApiDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddGraphQLServer()
    .RegisterDbContext<UniversityApiDbContext>()
    .AddType<StudentType>()
    .AddType<ProfessorType>()
    .AddType<CourseType>()
    .AddQueryType<Query>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());
});

builder.Host.UseSerilog((ctx,lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPeopleRespository, PeopleRepository>();
builder.Services.AddScoped<IProfessorsRepository, ProfessorsRepository>();
builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();
builder.Services.AddScoped<ICoursesRepository, CoursesRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
    app.MapControllers();
});

app.Run();
