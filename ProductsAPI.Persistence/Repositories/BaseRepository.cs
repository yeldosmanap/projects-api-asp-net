using Microsoft.EntityFrameworkCore;
using ProductsAPI.Application.Contract.Persistence;
using ProductsAPI.Domain.Exceptions;

namespace ProductsAPI.Persistence.Repositories;

public class BaseRepository<T> : IAsyncRepository<T> where T : class
{
    protected readonly DataContext DataContext;

    protected BaseRepository(DataContext dataContext)
    {
        DataContext = dataContext;
    }

    public virtual async Task<T> GetByIdAsync(Guid id)
    {
        return await DataContext.Set<T>().FindAsync(id) ?? throw new EntityNotFoundException("Entity not found"); 
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await DataContext.Set<T>().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await DataContext.Set<T>().AddAsync(entity);
        await DataContext.SaveChangesAsync();
        
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        DataContext.Set<T>().Update(entity);
        await DataContext.SaveChangesAsync();
        
        return entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        DataContext.Set<T>().Remove(entity);
        DataContext.Entry(entity).State = EntityState.Deleted;
        
        await DataContext.SaveChangesAsync();

        return entity;
    }
}