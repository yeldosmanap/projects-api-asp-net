using ProductsAPI.Domain.Enums;

namespace ProductsAPI.Application.DTO.Project;

public record UpdateProjectDto(
    string Name,
    DateTime StartDate,
    DateTime CompletionDate,
    ProjectStatus ProjectStatus,
    int Priority
);