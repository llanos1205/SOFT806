using SOFT703A2.Infrastructure.Contracts.Models;

namespace SOFT703A2.Domain.Models;

public class Trolley:IEntity
{
    public string? Id { set; get; }
    public  bool IsCurrent { get; set; }
    public string? UserId { get; set; } 
    public User? User { get; set; } 
    public List<ProductXTrolley>? ProductXTrolleys { get; set; } 
    public DateTime TransactionDate { get; set; }
    public double Total { get; set; }

}