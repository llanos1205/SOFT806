namespace SOFT806.Infrastructure.Contracts.ViewModels.Home;

using SOFT806.Domain.Models;

public interface IHomeViewModel
{
    public List<Product>? PromotedProducts { get; set; }
    public Task Load();
}