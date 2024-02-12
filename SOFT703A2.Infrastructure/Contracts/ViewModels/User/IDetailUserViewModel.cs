
using SOFT703A2.Infrastructure.ViewModels.Shared;

namespace SOFT703A2.Infrastructure.Contracts.ViewModels.User;

public interface IDetailUserViewModel
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? SelectedRole { get; set; }
    public List<DropdownOption> Roles { get; set; }
    public List<Domain.Models.Trolley>? Trolleys { get; set; }
    public Task<bool> Update();
    public Task LoadRoles();
    public Task<bool> Find(string id);
}