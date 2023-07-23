using System.ComponentModel;

namespace ProductsAPI.Domain.Enums;

public enum ProjectStatus
{
    [Description("Project has not started yet")]
    NotStarted,
    [Description("Project is in active development")]
    Active,
    [Description("Project is completed")]
    Completed
}