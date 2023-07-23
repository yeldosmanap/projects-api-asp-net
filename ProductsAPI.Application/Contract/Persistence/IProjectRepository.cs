using ProductsAPI.Domain.Entities;

namespace ProductsAPI.Application.Contract.Persistence;

public interface IProjectRepository<T> : IAsyncRepository<T> where T : class
{
    
}