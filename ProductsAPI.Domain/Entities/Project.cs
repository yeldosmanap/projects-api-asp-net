using ProductsAPI.Domain.Common;
using ProductsAPI.Domain.Enums;

namespace ProductsAPI.Domain.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; }
    
    public DateTimeOffset StartDate { get; set; }
    
    public DateTimeOffset CompletionDate { get; set; }
    
    public ProjectStatus Status { get; set; }
    
    public int Priority { get; set; }

    public IEnumerable<EntityTask> Tasks { get; set; } = null!;
}

