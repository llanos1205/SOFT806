using Microsoft.AspNetCore.Mvc;
using SOFT806.Infrastructure.Contracts.ViewModels.Trolley;

namespace SOFT806.WebApp.Controllers;

public class TrolleyController : Controller
{
    // GET
    private readonly ITrolleyViewModel _trolleyViewModel;
    private readonly ILogger<TrolleyController> _logger;

    public TrolleyController(ITrolleyViewModel trolleyViewModel, ILogger<TrolleyController> logger)
    {
        _trolleyViewModel = trolleyViewModel;
        _logger = logger;
    }

    public async Task<IActionResult> Detail(string id)
    {
        try
        {
            _logger.LogInformation("Detail called");
            await _trolleyViewModel.GetByIdAsync(id);
            return View(_trolleyViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return RedirectToAction("Error500", "Error");
        }
    }
}