namespace ProjectsAPI.DTO.Project;

public record CreateProjectDto(
    string Name,
    DateTime StartDate,
    DateTime CompletionDate,
    int Priority
);