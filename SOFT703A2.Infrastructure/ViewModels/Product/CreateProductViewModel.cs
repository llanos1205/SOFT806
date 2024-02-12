using System.ComponentModel.DataAnnotations;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Product;
using SOFT703A2.Infrastructure.ViewModels.Shared;

namespace SOFT703A2.Infrastructure.ViewModels.Product;

using SOFT703A2.Domain.Models;

public class CreateProductViewModel : ICreateProductViewModel
{
    [Required] [MaxLength(64)]public string? Name { get; set; }
    [Required] public string? Photo { get; set; }
    [Required] public bool IsPromoted { get; set; }
    [Required] public int Stock { get; set; }
    [Required] public double Price { get; set; }
    public List<DropdownOption>? Categories { get; set; }
    [Required] public string SelectedCategory { get; set; }

    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateProductViewModel(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public CreateProductViewModel()
    {
    }

    public async Task<bool> Create()
    {
        var result = await _productRepository.AddAsync(new Product()
        {
            Name = this.Name,
            Photo = this.Photo,
            Stock = this.Stock,
            Price = this.Price,
            IsPromoted = this.IsPromoted,
            CategoryId = this.SelectedCategory
        });
        return result != null;
    }

    public async Task<bool> Delete(string id)
    {
        var result = await _productRepository.DeleteAsync(id);
        return result != null;
    }

    public async Task LoadCategories()
    {
        var categories = await _categoryRepository.GetAllAsync();
        Categories = categories.Select(c => new DropdownOption { Value = c.Id.ToString(), Text = c.Name }).ToList();
    }
}