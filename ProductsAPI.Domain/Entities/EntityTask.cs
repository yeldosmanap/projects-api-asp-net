using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ProductsAPI.Domain.Common;
using ProductsAPI.Domain.Enums;

namespace ProductsAPI.Domain.Entities;

[Table("Tasks")]
public class EntityTask : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }
    
    public EntityTaskStatus Status { get; set; }

    public int Priority { get; set; }
    
    public Guid TaskProjectId { get; set; }
    
    [JsonIgnore]
    public Project TaskProject { get; set; }
}