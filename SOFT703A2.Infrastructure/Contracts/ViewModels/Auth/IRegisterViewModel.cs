namespace SOFT703A2.Infrastructure.Contracts.ViewModels.Auth;

public interface IRegisterViewModel
{

    
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? VerifyPassword { get; set; }
    public Task<bool> Register();
    public bool VerifyPasswordMatch();

}