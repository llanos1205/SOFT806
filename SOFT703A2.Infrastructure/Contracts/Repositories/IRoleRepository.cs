using SOFT703A2.Domain.Models;
using SOFT703A2.Infrastructure.Repositories;

namespace SOFT703A2.Infrastructure.Contracts.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    public Task<string> GetRoleId(string roleName);
}