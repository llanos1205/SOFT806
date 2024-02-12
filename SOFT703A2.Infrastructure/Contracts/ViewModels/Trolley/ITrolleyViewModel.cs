namespace SOFT703A2.Infrastructure.Contracts.ViewModels.Trolley;
using SOFT703A2.Domain.Models;
public interface ITrolleyViewModel
{
    public Trolley Trolley { get; set; }
    public Task GetByIdAsync(string id);
}