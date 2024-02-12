using SOFT703A2.Infrastructure.Contracts.Models;
using SOFT703A2.Domain.Models;
namespace SOFT703A2.Infrastructure.Contracts.Repositories;

public interface ITrolleyRepository:IBaseRepository<Trolley>
{
    Task<Trolley?> GetLatest(string id);
    Task<Trolley> RecalculateTotal(string id);
    Task<Trolley> AddProduct(string id, string productId);
    Task<Trolley> RemoveProduct(string id, string productId);
    Task<Trolley> CheckOut(string trolleyId);
    Task<Trolley?> GetExtendedTrolley(string trolleyId);
    
}