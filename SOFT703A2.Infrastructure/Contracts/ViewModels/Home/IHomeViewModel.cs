namespace SOFT703A2.Infrastructure.Contracts.ViewModels.Home;

using SOFT703A2.Domain.Models;

public interface IHomeViewModel
{
    public List<Product>? PromotedProducts { get; set; }
    public Task Load();
}