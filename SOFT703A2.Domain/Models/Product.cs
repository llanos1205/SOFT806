using SOFT703A2.Infrastructure.Contracts.Models;

namespace SOFT703A2.Domain.Models;

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