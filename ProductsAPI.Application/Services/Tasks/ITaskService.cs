using ProductsAPI.Domain.Entities;
using ProjectsAPI.DTO.Task;

namespace ProductsAPI.Application.Contract.Services;

public interface ITaskService : IService<EntityTask>
{
    public Task<EntityTask> Update(UpdateTaskDto updateTaskDto);
    public Task<EntityTask> Delete(Guid id);
    public Task<EntityTask> GetById(Guid id);
    public Task<IEnumerable<EntityTask>> GetAll();
}