using Microsoft.EntityFrameworkCore;
using SOFT703A2.Domain.Models;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Persistence;

namespace SOFT703A2.Infrastructure.Repositories;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<string> GetRoleId(string roleName)
    {
        Role roleFound = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        return roleFound.Id;
    }
}