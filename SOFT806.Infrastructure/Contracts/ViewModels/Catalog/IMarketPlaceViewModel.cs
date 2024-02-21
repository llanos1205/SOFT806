namespace SOFT806.Infrastructure.Contracts.ViewModels.Catalog;

using SOFT806.Domain.Models;

public interface IMarketPlaceViewModel
{
    public List<Product> Catalog { get; set; }
    public Trolley? CurrentTrolley { get; set; }
    public Task GetTrolley();
    public Task CheckOut(string trolleyId);
    public Task RemoveFromTrolley(string productId);
    public Task AddToTrolley(string productId);
    public Task GetAllAsync();
    public Task UpdateCatalog(string productName, bool byCategory, bool byPromoted, string orderBy);
}