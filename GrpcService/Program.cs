using GrpcService.Configurations;
using GrpcService.Database;
using GrpcService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GrpcDbContext>(opt => opt.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GrpcUniversityDb;Trusted_Connection=True;MultipleActiveResultSets=True"));

builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services.AddAutoMapper(typeof(AutomapperConfiguration));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GradesService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
