using SOFT703A2.Domain.Models;
using SOFT703A2.Infrastructure.Contracts.Models;

namespace SOFT703A2.Infrastructure.Contracts.Repositories;

public interface IUserRepository: IBaseRepository<User>
{
    public  Task<bool> Login(string Email,string Password);
    public  Task<bool> SignIn(User user, string Password);
    public Task LogOut();
    public string? GetUserId();
    Task<User?> GetUserTrolleyTransaction(string? id);
    Task SetRole(string email, string roleName);
    Task AddDefaultAsync(User user, string? password);

    Task<List<User>> GetExtendedSearch(string userName, bool byVisit, bool byEmail, bool byPhone);
    Task<string?> GetRoleId(string userId);
    
    Task<User> GetUserByEmail(string email);
}