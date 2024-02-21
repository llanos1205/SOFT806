namespace SOFT806.Infrastructure.Contracts.ViewModels.Product;
using SOFT806.Domain.Models;
public interface IListProductViewModel
{
    public List<Product>? Products { get; set; }

    public Task GetAll();
    public Task GetByName(string name);
}