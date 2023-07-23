using ProductsAPI.Domain.Enums;
using TaskStatus = ProjectsAPI;

namespace ProductsAPI.Application.DTO.Task;

public record CreateTaskDto(
    string Name,
    string Description,
    EntityTaskStatus Status,
    int Priority
);