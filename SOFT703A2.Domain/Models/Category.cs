using SOFT703A2.Infrastructure.Contracts.Models;

namespace SOFT703A2.Domain.Models;

public class Category:IEntity
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public List<Product>? Products { get; set; }
    
}