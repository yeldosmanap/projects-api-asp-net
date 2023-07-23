using ProductsAPI.Application.Contract.Persistence;
using ProductsAPI.Domain.Entities;

namespace ProductsAPI.Persistence.Repositories;

public class ProjectsRepository : BaseRepository<Project>, IProjectRepository<Project>
{
    protected ProjectsRepository(DataContext dataContext) : base(dataContext)
    {
    }
}