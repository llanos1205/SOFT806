using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Home;
using SOFT703A2.Infrastructure.ViewModels.Shared;


namespace SOFT703A2.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHomeViewModel _homeViewModel;

    public HomeController(ILogger<HomeController> logger, IHomeViewModel homeViewModel)
    {
        _logger = logger;
        _homeViewModel = homeViewModel;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        await _homeViewModel.Load();
        return View(_homeViewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}