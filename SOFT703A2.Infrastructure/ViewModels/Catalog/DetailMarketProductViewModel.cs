using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Catalog;

namespace SOFT703A2.Infrastructure.ViewModels.Catalog;
using SOFT703A2.Domain.Models;
public class DetailMarketProductViewModel: IDetailMarketProductViewModel
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Photo { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    public string? CategoryName { get; set; }


    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public DetailMarketProductViewModel(IProductRepository ip,ICategoryRepository ic)
    {
        _productRepository = ip;
        _categoryRepository = ic;
    }
    
    public async Task FindProduct(string productId)
    {
        Product product = new Product();
        product = await _productRepository.GetByIdAsync(productId);
        this.Id = product.Id;
        this.Name = product.Name;
        this.Photo = product.Photo;
        this.Stock = product.Stock;
        this.Price = product.Price;

        Category category = new Category();
        category = await _categoryRepository.GetByIdAsync(product.CategoryId);
        this.CategoryName = category.Name;
    }
}