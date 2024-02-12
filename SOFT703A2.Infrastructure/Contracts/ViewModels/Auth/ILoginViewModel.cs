using System.ComponentModel.DataAnnotations;

namespace SOFT703A2.Infrastructure.Contracts.ViewModels.Auth;

public interface ILoginViewModel
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public Task<bool> Login();
    public Task LogOut();
}