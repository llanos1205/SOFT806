using System.ComponentModel.DataAnnotations;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Product;
using SOFT703A2.Infrastructure.ViewModels.Shared;

namespace SOFT703A2.Infrastructure.ViewModels.Product;

public class DetailProductViewModel : IDetailProductViewModel
{
    [Required] public string? Id { get; set; }
    [Required] [MaxLength(64)] public string? Name { get; set; }
    [Required] public string? Photo { get; set; }
    [Required] public int Stock { get; set; }
    [Required] public double Price { get; set; }
    [Required] public bool IsPromoted { get; set; }

    public List<DropdownOption>? Categories { get; set; }
    [Required] public string SelectedCategory { get; set; }

    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public DetailProductViewModel(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }


    public DetailProductViewModel()
    {
    }

    public async Task<bool> Update(string id)

    {
        var product = await _productRepository.GetByIdAsync(id);
        product.Name = this.Name;
        product.Photo = this.Photo;
        product.Stock = this.Stock;
        product.Price = this.Price;
        product.IsPromoted = this.IsPromoted;
        product.CategoryId = this.SelectedCategory;
        var result = await _productRepository.UpdateAsync(product);
        return result != null;
    }

    public async Task<bool> Find(string id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product != null)
        {
            this.Name = product.Name;
            this.Photo = product.Photo;
            this.Stock = product.Stock;
            this.Price = product.Price;
            this.Id = product.Id;
            this.IsPromoted = product.IsPromoted;
            this.SelectedCategory = product.CategoryId;
            return true;
        }

        return false;
    }

    public async Task Promote(string id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        product.IsPromoted = true;
        await _productRepository.UpdateAsync(product);
    }

    public async Task UnPromote(string id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        product.IsPromoted = false;
        await _productRepository.UpdateAsync(product);
    }

    public async Task LoadCategories()
    {
        var categories = await _categoryRepository.GetAllAsync();
        Categories = categories.Select(c => new DropdownOption { Value = c.Id.ToString(), Text = c.Name }).ToList();
    }
}