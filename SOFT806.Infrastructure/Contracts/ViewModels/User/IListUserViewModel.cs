namespace SOFT806.Infrastructure.Contracts.ViewModels.User;
using SOFT806.Domain.Models;
public interface IListUserViewModel
{
    public List<User> Users { get; set; }
    public Task GetAllAsync();
    public Task UpdateUsersList(string userName, bool byVisit, bool byEmail, bool byPhone, string sortBy);
}