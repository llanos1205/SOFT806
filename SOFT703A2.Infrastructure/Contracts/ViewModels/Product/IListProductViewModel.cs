namespace SOFT703A2.Infrastructure.Contracts.ViewModels.Product;
using SOFT703A2.Domain.Models;
public interface IListProductViewModel
{
    public List<Product>? Products { get; set; }

    public Task GetAll();
    public Task GetByName(string name);
}