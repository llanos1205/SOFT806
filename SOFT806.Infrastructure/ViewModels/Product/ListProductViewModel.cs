using SOFT806.Infrastructure.Contracts.Repositories;
using SOFT806.Infrastructure.Contracts.ViewModels.Product;

namespace SOFT806.Infrastructure.ViewModels.Product;
using SOFT806.Domain.Models;
public class ListProductViewModel:IListProductViewModel
{
    public List<Product>? Products { get; set; }
    private readonly IProductRepository _productRepository;
    public ListProductViewModel(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public ListProductViewModel()
    {
        
    }
    public async Task GetAll()
    {
        Products = await _productRepository.GetAllWithCategoriesAsync();
    }

    public Task GetByName(string name)
    {
        throw new NotImplementedException();
    }
}