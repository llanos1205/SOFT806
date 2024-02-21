using SOFT806.Domain.Contracts;

namespace SOFT806.Domain.Models;


public class Category:IEntity
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public List<Product>? Products { get; set; }
    
}