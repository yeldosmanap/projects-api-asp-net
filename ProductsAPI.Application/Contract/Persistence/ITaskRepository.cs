using ProductsAPI.Domain.Entities;

namespace ProductsAPI.Application.Contract.Persistence;

public interface ITaskRepository<T> : IAsyncRepository<T> where T : class
{
    Task<IEnumerable<T>> GetTasksByProjectId(Guid projectId);
}