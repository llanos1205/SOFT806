using SOFT806.Domain.Contracts;
using SOFT806.Domain.Models;
namespace SOFT806.Infrastructure.Contracts.Repositories;

public interface IProductRepository: IBaseRepository<Product>
{
    public Task<List<Product>> GetExtendedSearch(string name, bool byCategory, bool byPromoted);
    public Task<List<Product>> GetAllWithCategoriesAsync();

}