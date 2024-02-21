using Microsoft.AspNetCore.Identity;
using SOFT806.Domain.Contracts;

namespace SOFT806.Domain.Models;

public class User: IdentityUser,IEntity
{
    public  string? FirstName { get; set; }
    public  string? LastName { get; set; }
    public  string? Phone { get; set; }
    public List<Trolley>? Trolleys { get; set; } 
    public List<Login>? Logins { get; set; }

}