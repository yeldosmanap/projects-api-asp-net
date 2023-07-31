using Microsoft.EntityFrameworkCore;
using ProductsAPI.Application.Contract.Persistence;
using ProductsAPI.Domain.Entities;
using ProductsAPI.Persistence.Context;

namespace ProductsAPI.Persistence.Repositories;

public class TasksRepository : BaseRepository<EntityTask>, ITaskRepository<EntityTask>
{
    public TasksRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public async Task<IEnumerable<EntityTask>> GetTasksByProjectId(Guid projectId)
    {
        return await 
            Task.FromResult<IEnumerable<EntityTask>>(
                await DataContext.Tasks
                .Where(task => task.TaskProjectId == projectId)
                .ToListAsync());
    }
}