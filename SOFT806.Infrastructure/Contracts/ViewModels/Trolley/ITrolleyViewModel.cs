namespace SOFT806.Infrastructure.Contracts.ViewModels.Trolley;
using SOFT806.Domain.Models;
public interface ITrolleyViewModel
{
    public Trolley Trolley { get; set; }
    public Task GetByIdAsync(string id);
}