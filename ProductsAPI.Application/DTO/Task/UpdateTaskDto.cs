namespace ProjectsAPI.DTO.Task;

public record UpdateTaskDto(
    Guid Id, 
    string Name,
    string Description,
    int Priority,
    Guid TaskProjectId
);