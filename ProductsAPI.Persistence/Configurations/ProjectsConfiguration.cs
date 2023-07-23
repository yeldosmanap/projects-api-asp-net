using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsAPI.Domain.Entities;
using ProductsAPI.Domain.Enums;

namespace ProductsAPI.Persistence.Configurations;

public class ProjectsConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> modelBuilder)
    {
        modelBuilder
            .Property(p => p.Status)
            .HasDefaultValue(ProjectStatus.NotStarted)
            .HasConversion(p => p.ToString(),
                p => (ProjectStatus)Enum.Parse(typeof(ProjectStatus), p))
            .IsRequired();

        modelBuilder
            .HasMany(p => p.Tasks)
            .WithOne(t => t.TaskProject)
            .HasForeignKey(t => t.TaskProjectId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}