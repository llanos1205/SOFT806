﻿using System.ComponentModel.DataAnnotations;
using SOFT806.Infrastructure.Contracts.Repositories;
using SOFT806.Infrastructure.Contracts.ViewModels;
using SOFT806.Infrastructure.Contracts.ViewModels.Auth;

namespace SOFT806.Infrastructure.ViewModels.Auth;

public class LoginViewModel:ILoginViewModel
{
    
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    
    private readonly IUserRepository _userRepository;
    public LoginViewModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public LoginViewModel()
    {
        
    }
    public async Task<bool> Login()
    {
        return await _userRepository.Login(this.Email, this.Password);
    }

    public async Task LogOut()
    {
        await _userRepository.LogOut();
    }
}