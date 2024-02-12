using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Auth;
using SOFT703A2.Infrastructure.ViewModels.Auth;

namespace SOFT703A2.WebApp.Controllers;

public class AccountController : Controller
{
    private readonly ILoginViewModel _loginViewModel;
    private readonly IRegisterViewModel _registerViewModel;
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILoginViewModel loginViewModel, IRegisterViewModel registerViewModel,
        ILogger<AccountController> logger)
    {
        _loginViewModel = loginViewModel;
        _logger = logger;
        _registerViewModel = registerViewModel;
    }

    public IActionResult Login()
    {
        return View(_loginViewModel);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        try
        {
            _logger.LogInformation("Login attempt");
            if (ModelState.IsValid)
            {
                _loginViewModel.Email = viewModel.Email;
                _loginViewModel.Password = viewModel.Password;
                var result = await _loginViewModel.Login();
                if (result)
                {
                    _logger.LogInformation("Login successful");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login failed. Please try again.");
                }
            }

            return View(_loginViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return RedirectToAction("Error500", "Error");
        }
    }

    public IActionResult Register()
    {
        return View(_registerViewModel);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel viewModel)
    {
        try
        {
            _logger.LogInformation("Register attempt");
            if (ModelState.IsValid)
            {
                if (viewModel.VerifyPasswordMatch())
                {
                    _registerViewModel.FirstName = viewModel.FirstName;
                    _registerViewModel.LastName = viewModel.LastName;
                    _registerViewModel.PhoneNumber = viewModel.PhoneNumber;
                    _registerViewModel.Email = viewModel.Email;
                    _registerViewModel.Password = viewModel.Password;
                    var result = await _registerViewModel.Register();
                    if (result)
                    {
                        _logger.LogInformation("Register successful");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Registration failed. Please try again.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                }
            }

            return View(viewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return RedirectToAction("Error500", "Error");
        }
    }

    public IActionResult LogOut()
    {
        try
        {
            _logger.LogInformation("LogOut called");
            _loginViewModel.LogOut();
            return RedirectToAction("Index", "Home");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return RedirectToAction("Error500", "Error");
        }
    }
}