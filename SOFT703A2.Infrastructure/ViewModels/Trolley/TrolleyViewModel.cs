using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Trolley;

namespace SOFT703A2.Infrastructure.ViewModels.Trolley;
using SOFT703A2.Domain.Models;
public class TrolleyViewModel:ITrolleyViewModel
{
    public Trolley? Trolley { get; set; }
    
    private readonly ITrolleyRepository _trolleyRepository;
    public TrolleyViewModel(ITrolleyRepository trolleyRepository)
    {
        _trolleyRepository = trolleyRepository;
    }
    public TrolleyViewModel()
    {
        
    }
    public async Task GetByIdAsync(string id)
    {
        Trolley = await _trolleyRepository.GetExtendedTrolley(id);
    }
}