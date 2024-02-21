using Microsoft.EntityFrameworkCore;
using SOFT806.Domain.Models;
using SOFT806.Infrastructure.Contracts.Repositories;
using SOFT806.Infrastructure.Persistence;

namespace SOFT806.Infrastructure.Repositories;

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