using ProductsAPI.Application.DTO.Project;
using ProductsAPI.Application.DTO.Task;
using ProductsAPI.Domain.Entities;
using ProjectsAPI.DTO.Project;

namespace ProductsAPI.Application.Services.Projects;

public interface IProjectService : IService<Project>
{
    public Task<Project> Add(CreateProjectDto createProjectDto);
    public Task<Project> Update(Guid projectId, UpdateProjectDto updateProjectDto);
    public Task<Project> Delete(Guid projectId);
    public Task<Project> GetById(Guid id);
    public Task<IEnumerable<Project>> GetAll();
    public Task<Project> AddTaskToProject(Guid projectId, CreateTaskDto createTaskDto);
    public Task<IEnumerable<EntityTask>> GetTasks(Guid projectId);
}