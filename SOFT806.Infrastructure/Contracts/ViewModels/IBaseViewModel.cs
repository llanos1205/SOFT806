namespace SOFT806.Infrastructure.Contracts.ViewModels;

public interface IBaseViewModel
{
    Task DeleteAsync(string id);
    Task GetByIdAsync(string id);
    Task GetAllAsync();
    Task AddAsync();
    Task UpdateAsync(string id);

}