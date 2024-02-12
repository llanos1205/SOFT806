using Microsoft.IdentityModel.Tokens;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Contracts.ViewModels.User;

namespace SOFT703A2.Infrastructure.ViewModels.User;

public class ListUserViewModel:IListUserViewModel
{
    public List<Domain.Models.User> Users { get; set; }
    private readonly IUserRepository _userRepository;
    public ListUserViewModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public ListUserViewModel()
    {
    }
    public async Task GetAllAsync()
    {
        Users = await _userRepository.GetExtendedSearch("",false,false,false);
    }

    public async Task UpdateUsersList(string userName, bool byVisit, bool byEmail, bool byPhone, string sortBy)
    {
        Users = await _userRepository.GetExtendedSearch(userName, byVisit, byEmail, byPhone);
        if (!string.IsNullOrEmpty(sortBy) && !Users.IsNullOrEmpty())
        {
            switch (sortBy)
            {
                case "nameSort":
                    Users = Users.OrderBy(p => p.LastName).ToList();
                    break;
                case "loginSort":
                    Users = Users.OrderByDescending(p => p.Logins.Count).ToList();
                    break;
                case "usernameSort":
                    Users = Users.OrderBy(p => p.UserName).ToList();
                    break;
            }
        }
    }
}