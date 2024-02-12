
using SOFT703A2.Infrastructure.ViewModels.Shared;

namespace SOFT703A2.Infrastructure.Contracts.ViewModels.Product;

public interface IDetailProductViewModel
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Photo { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    public bool IsPromoted { get; set; }
    public List<DropdownOption>? Categories { get; set; }
    public string SelectedCategory { get; set; }
    public Task<bool> Update(string id);
    public Task<bool> Find(string id);
    public Task Promote(string id);
    public Task UnPromote(string id);
    public Task LoadCategories();
}