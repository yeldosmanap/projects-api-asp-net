using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsAPI.Domain.Entities;
using ProductsAPI.Domain.Enums;

namespace ProductsAPI.Persistence.Configurations;

public class TasksConfiguration : IEntityTypeConfiguration<EntityTask>
{
    public void Configure(EntityTypeBuilder<EntityTask> modelBuilder)
    {
        modelBuilder
            .Property(t => t.Status)
            .HasDefaultValue(EntityTaskStatus.ToDo).
            HasConversion(t => t.ToString(),
                t => (EntityTaskStatus)Enum.Parse(typeof(EntityTaskStatus), t)).
            IsRequired();
    }
}