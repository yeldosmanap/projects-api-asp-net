using ProductsAPI.Application.Contract.Persistence;
using ProductsAPI.Domain.Entities;
using ProductsAPI.Persistence.Context;

namespace ProductsAPI.Persistence.Repositories;

public class ProjectsRepository : BaseRepository<Project>, IProjectRepository<Project>
{
    public ProjectsRepository(DataContext dataContext) : base(dataContext)
    {
    }
}