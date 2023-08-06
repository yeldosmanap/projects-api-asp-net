using ProductsAPI.Application.Contract.Persistence;
using ProductsAPI.Application.Contract.Services;
using ProductsAPI.Application.DTO.Project;
using ProductsAPI.Application.DTO.Task;
using ProductsAPI.Domain.Entities;
using ProductsAPI.Domain.Exceptions;
using ProjectsAPI.DTO.Project;

namespace ProductsAPI.Persistence.Services;

public class ProjectsService : IProjectService
{
    private readonly IProjectRepository<Project> _projectsBaseRepository;
    private readonly ITaskRepository<EntityTask> _tasksBaseRepository;

    public ProjectsService(IProjectRepository<Project> projectsBaseRepository, ITaskRepository<EntityTask> tasksBaseRepository)
    {
        _projectsBaseRepository = projectsBaseRepository;
        _tasksBaseRepository = tasksBaseRepository;
    }

    public async Task<Project> GetById(Guid projectId)
    {
        var project = await _projectsBaseRepository.GetByIdAsync(projectId);
        if (project == null)
        {
            throw new EntityNotFoundException($"Project with id {projectId} not found");
        }

        project.Tasks = await _tasksBaseRepository.GetTasksByProjectId(projectId);

        return project;
    }

    public async Task<IEnumerable<Project>> GetAll()
    {
        return await _projectsBaseRepository.GetAllAsync();
    }

    public async Task<Project> Add(CreateProjectDto createProjectDto)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = createProjectDto.Name,
            StartDate = createProjectDto.StartDate,
            CompletionDate = createProjectDto.CompletionDate,
            Priority = createProjectDto.Priority
        };
        
        return await _projectsBaseRepository.AddAsync(project);
    }

    public async Task<Project> Update(Guid projectId, UpdateProjectDto updateProjectDto)
    {
        var project = await _projectsBaseRepository.GetByIdAsync(projectId);
        if (project == null)
        {
            throw new EntityNotFoundException($"Project with id {projectId} not found");
        }

        project.Tasks = await _tasksBaseRepository.GetTasksByProjectId(projectId);
        project.Name = updateProjectDto.Name;
        project.StartDate = updateProjectDto.StartDate;
        project.CompletionDate = updateProjectDto.CompletionDate;
        project.Status = updateProjectDto.ProjectStatus;
        project.Priority = updateProjectDto.Priority;
        
        return await _projectsBaseRepository.UpdateAsync(project);
    }

    public async Task<Project> Delete(Guid projectId)
    {
        var project = _projectsBaseRepository.GetByIdAsync(projectId).Result;
        
        if (project == null)
        {
            throw new EntityNotFoundException($"Project with id {projectId} not found");
        }
        
        return await _projectsBaseRepository.DeleteAsync(project);
    }

    public async Task<Project> AddTaskToProject(Guid projectId, CreateTaskDto createTaskDto)
    {
        var project = await GetExistingProjectAsync(projectId);
        
        var task = new EntityTask
        {
            Id = Guid.NewGuid(),
            Name = createTaskDto.Name,
            Description = createTaskDto.Description,
            Priority = createTaskDto.Priority,
            Status = createTaskDto.Status,
            TaskProject = project,
            TaskProjectId = project.Id
        };
        
        await _tasksBaseRepository.AddAsync(task);
        return await _projectsBaseRepository.UpdateAsync(project);
    }

    public async Task<IEnumerable<EntityTask>> GetTasks(Guid projectId)
    {
        await GetExistingProjectAsync(projectId);
        
        return await _tasksBaseRepository.GetTasksByProjectId(projectId);
    }
    
    private async Task<Project> GetExistingProjectAsync(Guid projectId)
    {
        var project = await _projectsBaseRepository.GetByIdAsync(projectId);
        if (project == null)
        {
            throw new EntityNotFoundException($"Project with id {projectId} not found");
        }
        
        return project;
    }
}