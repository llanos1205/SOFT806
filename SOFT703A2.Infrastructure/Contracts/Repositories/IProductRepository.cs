using SOFT703A2.Infrastructure.Contracts.Models;
using SOFT703A2.Domain.Models;
namespace SOFT703A2.Infrastructure.Contracts.Repositories;

public interface IProductRepository: IBaseRepository<Product>
{
    public Task<List<Product>> GetExtendedSearch(string name, bool byCategory, bool byPromoted);
    public Task<List<Product>> GetAllWithCategoriesAsync();

}