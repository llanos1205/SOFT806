using SOFT806.Domain.Contracts;

namespace SOFT806.Domain.Models;

public class Login:IEntity
{
    public string? Id { get; set; }
    public DateTime? SessionDate { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
}