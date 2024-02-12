using SOFT703A2.Domain.Models;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Persistence;

namespace SOFT703A2.Infrastructure.Repositories;

public class CategoryRepository:BaseRepository<Category>,ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}