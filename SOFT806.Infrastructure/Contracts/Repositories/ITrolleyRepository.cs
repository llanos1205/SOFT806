using SOFT806.Domain.Contracts;
using SOFT806.Domain.Models;
namespace SOFT806.Infrastructure.Contracts.Repositories;

public interface ITrolleyRepository:IBaseRepository<Trolley>
{
    Task<Trolley?> GetLatest(string id);
    Task<Trolley> RecalculateTotal(string id);
    Task<Trolley> AddProduct(string id, string productId);
    Task<Trolley> RemoveProduct(string id, string productId);
    Task<Trolley> CheckOut(string trolleyId);
    Task<Trolley?> GetExtendedTrolley(string trolleyId);
    
}