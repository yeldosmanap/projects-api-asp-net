using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductsAPI.Domain.Entities;

namespace ProductsAPI.Persistence;

public class DataContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<Project> Projects { get; set; } = null!;
    
    public DbSet<EntityTask> Tasks { get; set; } = null!;

    public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) 
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(_configuration.GetConnectionString("Default"));

        options.EnableSensitiveDataLogging();
    }
}