using SOFT703A2.Infrastructure.ViewModels.Shared;

namespace SOFT703A2.Infrastructure.Contracts.ViewModels.User;

public interface ICreateUserViewModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? SelectedRole { get; set; }
    public List<DropdownOption> Roles { get; set; }
    public Task Create();
    public Task LoadRoles();
    Task<bool> Delete(string id);
}