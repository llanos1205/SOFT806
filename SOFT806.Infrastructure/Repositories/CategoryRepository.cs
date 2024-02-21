using SOFT806.Domain.Models;
using SOFT806.Infrastructure.Contracts.Repositories;
using SOFT806.Infrastructure.Persistence;

namespace SOFT806.Infrastructure.Repositories;

public class CategoryRepository:BaseRepository<Category>,ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}