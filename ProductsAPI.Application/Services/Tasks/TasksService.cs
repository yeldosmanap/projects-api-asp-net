using ProductsAPI.Application.Contract.Persistence;
using ProductsAPI.Application.Contract.Services;
using ProductsAPI.Domain.Entities;
using ProductsAPI.Domain.Exceptions;
using ProjectsAPI.DTO.Task;

namespace ProductsAPI.Persistence.Services;

public class TasksService : ITaskService
{
    private readonly IProjectRepository<Project> _projectsBaseRepository;
    private readonly ITaskRepository<EntityTask> _tasksBaseRepository;

    public TasksService(IProjectRepository<Project> projectsBaseRepository, ITaskRepository<EntityTask> tasksBaseRepository)
    {
        _projectsBaseRepository = projectsBaseRepository;
        _tasksBaseRepository = tasksBaseRepository;
    }

    public async Task<EntityTask> GetById(Guid taskId)
    {
        var task = await _tasksBaseRepository.GetByIdAsync(taskId)!;
        
        if (task == null)
        {
            throw new EntityNotFoundException($"Task with id {taskId} was not found");
        }

        task.TaskProject = await _projectsBaseRepository.GetByIdAsync(taskId)
                           ?? throw new EntityNotFoundException($"Project of task {task.TaskProjectId} not found");

        return task;
    }

    public async Task<IEnumerable<EntityTask>> GetAll()
    {
        return await _tasksBaseRepository.GetAllAsync();
    }

    public async Task<EntityTask> Update(UpdateTaskDto updateTaskDto)
    {
        var existingTask = await _tasksBaseRepository.GetByIdAsync(updateTaskDto.Id);

        if (existingTask == null)
        {
            throw new EntityNotFoundException($"Task with id {updateTaskDto.Id} was not found");
        }

        existingTask.Name = updateTaskDto.Name;
        existingTask.Description = updateTaskDto.Description;
        existingTask.Priority = updateTaskDto.Priority;
        existingTask.TaskProjectId = updateTaskDto.TaskProjectId;

        return await _tasksBaseRepository.UpdateAsync(existingTask);
    }

    public async Task<EntityTask> Delete(Guid taskId)
    {
        var task = await _tasksBaseRepository.GetByIdAsync(taskId);
        
        if (task == null)
        {
            throw new EntityNotFoundException($"Task with id {taskId} was not found");
        }
        
        return await _tasksBaseRepository.DeleteAsync(task);
    }
}