using SOFT806.Domain.Models;
using SOFT806.Infrastructure.Repositories;

namespace SOFT806.Infrastructure.Contracts.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    public Task<string> GetRoleId(string roleName);
}