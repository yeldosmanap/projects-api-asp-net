using ProductsAPI.Application.Contract.Persistence;
using ProductsAPI.Domain.Entities;

namespace ProductsAPI.Persistence.Repositories;

public class TasksRepository : BaseRepository<EntityTask>, ITaskRepository<EntityTask>
{
    protected TasksRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public async Task<IEnumerable<EntityTask>> GetTasksByProjectId(Guid projectId)
    {
        return await Task.FromResult<IEnumerable<EntityTask>>(
            DataContext
                .Tasks
                .Where(task => task.TaskProjectId == projectId)
                .ToList());
    }
}