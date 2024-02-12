namespace SOFT703A2.Infrastructure.Contracts.ViewModels.Catalog;

public interface IDetailMarketProductViewModel
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Photo { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    public string CategoryName { get; set; }

    public Task FindProduct(string productId);
}