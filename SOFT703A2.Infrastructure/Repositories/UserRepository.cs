using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SOFT703A2.Domain.Models;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Persistence;

namespace SOFT703A2.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager,
        ApplicationDbContext context) : base(context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> Login(string Email, string Password)
    {
        var user = await _userManager.FindByEmailAsync(Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, Password))
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            await _context.Login.AddAsync(
                new Login()
                {
                    SessionDate = DateTime.Now,
                    UserId = user.Id
                }
            );
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> SignIn(User user, string Password)
    {
        var result = await _userManager.CreateAsync(user, Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            var foundUser = await _userManager.FindByEmailAsync(user.Email);
            await _context.Login.AddAsync(
                new Login()
                {
                    SessionDate = DateTime.Now,
                    UserId = foundUser.Id
                }
            );
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task LogOut()
    {
        await _signInManager.SignOutAsync();
    }

    public string? GetUserId()
    {
        return _userManager.GetUserId(_signInManager.Context.User);
    }

    public Task<User?> GetUserTrolleyTransaction(string? id)
    {
        return id == null
            ? _context.Users
                .Include(x => x.Trolleys.Where(t => !t.IsCurrent).OrderBy(x => x.TransactionDate))
                .FirstOrDefaultAsync(x => x.Id == GetUserId())
            : _context.Users
                .Include(x => x.Trolleys.Where(t => !t.IsCurrent).OrderBy(x => x.TransactionDate))
                .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task SetRole(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);
        var result = await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task AddDefaultAsync(User user, string? password)
    {
        var result = await _userManager.CreateAsync(user, password);
    }

    public async Task<List<User>> GetExtendedSearch(string userName, bool byVisit, bool byEmail, bool byPhone)
    {
        try
        {
            var query = _context.Users.AsQueryable();

            if (byVisit)
            {
                query = query.OrderByDescending(user => user.Logins.Count).Take(3);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    if (byEmail)
                    {
                        query = query.Where(user => user.Email.ToLower().Contains(userName));
                    }
                    else if (byPhone)
                    {
                        query = query.Where(user => user.PhoneNumber.ToLower().Contains(userName));
                    }
                    else
                    {
                        query = query.Where(user => user.FirstName.ToLower().Contains(userName) ||
                                                    user.LastName.ToLower().Contains(userName) ||
                                                    user.UserName.ToLower().Contains(userName));
                    }
                }
            }

            // Execute the query and return the filtered products as a list
            var filteredProducts = await query
                .Select(user => new User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    Logins = user.Logins
                })
                .ToListAsync();

            return filteredProducts;
        }
        catch (Exception ex)
        {
            // do something here
            throw;
        }
    }

    public async Task<string?> GetRoleId(string userId)
    {
        var role = await _context.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId);
        return role.RoleId;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
}