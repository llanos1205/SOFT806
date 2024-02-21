using SOFT806.Domain.Contracts;

namespace SOFT806.Domain.Models;

public class Product : IEntity
{
    public string? Id { set; get; }
    public string? Name { get; set; }
    public string? Photo { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    public bool IsPromoted { get; set; } = false;
    public string? CategoryId { get; set; }
    public Category? Category { get; set; }
    public List<ProductXTrolley>? ProductXTrolleys { get; set; }
}