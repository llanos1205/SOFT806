using System.ComponentModel.DataAnnotations;

using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Auth;

namespace SOFT703A2.Infrastructure.ViewModels.Auth;
using SOFT703A2.Domain.Models;
public class RegisterViewModel:IRegisterViewModel
{
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? PhoneNumber { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 8 and 15 characters and contain at least one uppercase letter, one lowercase letter, one digit and one special character.")]
    public string? Password { get; set; }
    [Required]
    public string? VerifyPassword { get; set; }
    
    private readonly IUserRepository _userRepository;
    public RegisterViewModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public RegisterViewModel()
    {
        
    }

    public async Task<bool> Register()
    {
        var result = await _userRepository.SignIn(new User()
        {
            FirstName = FirstName,
            LastName = LastName,
            PhoneNumber = PhoneNumber,
            Email = Email,
            UserName = Email,
            
        },Password);
        await _userRepository.SetRole(Email, "Client");
        return result!=null;
    }

    public bool VerifyPasswordMatch()
    {
        return Password == VerifyPassword;
    }
}