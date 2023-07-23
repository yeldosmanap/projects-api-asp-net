using System.ComponentModel;

namespace ProductsAPI.Domain.Enums;

public enum EntityTaskStatus
{
    [Description("Task is not started yet")]
    ToDo,
    [Description("Task is in progress")]
    InProgress,
    [Description("Task is completed")]
    Done
}