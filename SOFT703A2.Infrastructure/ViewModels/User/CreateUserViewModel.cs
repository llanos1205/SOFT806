using System.ComponentModel.DataAnnotations;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Contracts.ViewModels.User;
using SOFT703A2.Infrastructure.ViewModels.Shared;

namespace SOFT703A2.Infrastructure.ViewModels.User;

using SOFT703A2.Domain.Models;

public class CreateUserViewModel : ICreateUserViewModel
{
    [Required] [MaxLength(64)] public string? FirstName { get; set; }
    [Required] [MaxLength(64)] public string? LastName { get; set; }

    [Required]
    [MaxLength(64)]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please enter valid phone number")]
    public string? PhoneNumber { get; set; }

    [Required] [EmailAddress] public string? Email { get; set; }
    [Required] public string? SelectedRole { get; set; }
    public List<DropdownOption>? Roles { get; set; }

    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public CreateUserViewModel(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public CreateUserViewModel()
    {
    }

    public async Task Create()
    {
        await _userRepository.AddDefaultAsync(new User()
        {
            FirstName = FirstName,
            LastName = LastName,
            PhoneNumber = PhoneNumber,
            Email = Email,
            UserName = Email,
        }, "Password123!");
        var role = await _roleRepository.GetByIdAsync(SelectedRole);
        await _userRepository.SetRole(Email, role.Name);
    }

    public async Task LoadRoles()
    {
        var rols = await _roleRepository.GetAllAsync();
        Roles = rols.Select(x => new DropdownOption()
        {
            Value = x.Id,
            Text = x.Name
        }).ToList();
    }

    public async Task<bool> Delete(string id)
    {
        var result = await _userRepository.DeleteAsync(id);
        return result != null;
    }
}