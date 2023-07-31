using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductsAPI.Domain.Entities;

namespace ProductsAPI.Persistence.Context;

public class DataContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<Project> Projects { get; set; } = null!;
    
    public DbSet<EntityTask> Tasks { get; set; } = null!;

    public DataContext()
    {
        
    }

    public DataContext(IConfiguration configuration) 
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(_configuration.GetConnectionString("Default"));

        options.EnableSensitiveDataLogging();
    }
}