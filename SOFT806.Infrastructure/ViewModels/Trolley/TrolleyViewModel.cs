﻿using SOFT806.Infrastructure.Contracts.Repositories;
using SOFT806.Infrastructure.Contracts.ViewModels.Trolley;

namespace SOFT806.Infrastructure.ViewModels.Trolley;
using SOFT806.Domain.Models;
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