using SOFT703A2.Infrastructure.Contracts.Models;

namespace SOFT703A2.Domain.Models;
public class ProductXTrolley
{
    public string? ProductId { get; set; }
    public Product? Product { get; set; }

    public string? TrolleyId { get; set; }
    public Trolley? Trolley { get; set; }
    public  int Quantity { get; set; }
}