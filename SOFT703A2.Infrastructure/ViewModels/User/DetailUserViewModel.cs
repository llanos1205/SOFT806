using System.ComponentModel.DataAnnotations;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Contracts.ViewModels.User;
using SOFT703A2.Infrastructure.ViewModels.Shared;

namespace SOFT703A2.Infrastructure.ViewModels.User;

using SOFT703A2.Domain.Models;

public class DetailUserViewModel : IDetailUserViewModel
{
    [Required] public string? Id { get; set; }
    [Required] [MaxLength(64)] public string? FirstName { get; set; }
    [Required] [MaxLength(64)] public string? LastName { get; set; }

    [Required]
    [MaxLength(64)]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please enter valid phone number")]
    public string? PhoneNumber { get; set; }

    [Required] [EmailAddress] public string? Email { get; set; }
    [Required] public string? SelectedRole { get; set; }
    public List<DropdownOption>? Roles { get; set; }
    public List<Trolley>? Trolleys { get; set; }

    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public DetailUserViewModel(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public DetailUserViewModel()
    {
    }

    public async Task<bool> Update()
    {
        var user = await _userRepository.GetByIdAsync(Id);
        user.UserName = Email;
        user.Email = Email;
        user.FirstName = FirstName;
        user.LastName = LastName;
        user.PhoneNumber = PhoneNumber;

        var result = await _userRepository.UpdateAsync(user);
        var role = await _roleRepository.GetByIdAsync(SelectedRole);
        await _userRepository.SetRole(Email, role.Name);
        return result != null;
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

    public async Task<bool> Find(string id)
    {
        var user = await _userRepository.GetUserTrolleyTransaction(id);
        var role = await _userRepository.GetRoleId(user.Id);
        if (user != null)
        {
            this.Email = user.UserName;
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.PhoneNumber = user.PhoneNumber;
            this.Id = user.Id;
            this.SelectedRole = role;
            if (user.Trolleys == null)
            {
                Trolleys = new List<Trolley>();
            }
            else
            {
                Trolleys = user.Trolleys;
            }

            return true;
        }

        return false;
    }
}