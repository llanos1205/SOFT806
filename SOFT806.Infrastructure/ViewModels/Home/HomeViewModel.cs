using SOFT806.Infrastructure.Contracts.Repositories;
using SOFT806.Infrastructure.Contracts.ViewModels.Home;

namespace SOFT806.Infrastructure.ViewModels.Home;

using SOFT806.Domain.Models;

public class HomeViewModel : IHomeViewModel
{
    public List<Product>? PromotedProducts { get; set; }

    private readonly IProductRepository _productRepository;

    public HomeViewModel(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public HomeViewModel()
    {
    }

    public async Task Load()
    {
        PromotedProducts = await _productRepository.GetAllWithCategoriesAsync();
        if (PromotedProducts != null)
        {
            if (PromotedProducts.Count != 0)
            {
                PromotedProducts = PromotedProducts.Where(p => p.IsPromoted).ToList();
            }
            else
            {
                PromotedProducts = new List<Product>();
            }
        }
        else
        {
            PromotedProducts = new List<Product>();
        }
    }
}