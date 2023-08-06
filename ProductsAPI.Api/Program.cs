using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductsAPI.Application.Contract.Persistence;
using ProductsAPI.Application.Services.Projects;
using ProductsAPI.Application.Services.Tasks;
using ProductsAPI.Domain.Entities;
using ProductsAPI.Persistence.Context;
using ProductsAPI.Persistence.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DataContext
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Add ProjectsRepository layer
builder.Services.AddScoped<IProjectRepository<Project>, ProjectsRepository>();
// Add TasksRepository layer
builder.Services.AddScoped<ITaskRepository<EntityTask>, TasksRepository>();

// Add ProjectsService layer
builder.Services.AddScoped<IProjectService, ProjectsService>();
// Add TasksService layer
builder.Services.AddScoped<ITaskService, TasksService>();

// Add JsonOptions for better readability of JSON responses in Swagger UI and Postman 
builder.Services.AddControllers().AddJsonOptions(options => 
{ 
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Use Serilog for logging
builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddAuthentication(services =>
{

    services.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    services.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    services.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});

var app = builder.Build();

// Add CORS policies
app.UseCors(corsPolicyBuilder => corsPolicyBuilder
    .WithMethods("POST", "GET", "PUT", "DELETE")
    .WithHeaders("Accept", "Content-Type", "Origin")
    .WithOrigins("http://localhost:5077", "https://localhost:5077"));

// Add serilog middleware for logging
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();