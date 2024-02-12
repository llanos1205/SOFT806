using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOFT703A2.Infrastructure.Commands;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Catalog;
using SOFT703A2.WebApp.Services;

namespace SOFT703A2.WebApp.Controllers;

[AllowAnonymous]
public class MarketPlaceController : Controller
{
    private readonly ILogger<MarketPlaceController> _logger;
    private readonly IMarketPlaceViewModel _marketPlaceViewModel;
    private readonly IDetailMarketProductViewModel _detailMarketProduct;

    public MarketPlaceController(IMarketPlaceViewModel vm, ILogger<MarketPlaceController> logger,
        IDetailMarketProductViewModel dmp)
    {
        _logger = logger;
        _marketPlaceViewModel = vm;
        _detailMarketProduct = dmp;
    }

    [CustomAuthorize]
    public async Task<IActionResult> Index()
    {
        try
        {
            _logger.LogInformation("Index called");
            await _marketPlaceViewModel.GetAllAsync();
            return View(_marketPlaceViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return RedirectToAction("Error500", "Error");
        }
    }

    [HttpGet]
    [CustomAuthorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToTrolley(string productId)
    {
        try
        {
            _logger.LogInformation("AddToTrolley called");
            await _marketPlaceViewModel.AddToTrolley(productId);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            string trolleyJson = JsonSerializer.Serialize(_marketPlaceViewModel.CurrentTrolley, options);
            _logger.LogInformation("AddToTrolley completed");
            return Content(trolleyJson, "application/json");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return RedirectToAction("Error500", "Error");
        }
    }

    public async Task<IActionResult> AddToTrolleyView(string id)
    {
        try
        {
            _logger.LogInformation("AddToTrolley called");
            await _marketPlaceViewModel.AddToTrolley(id);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            string trolleyJson = JsonSerializer.Serialize(_marketPlaceViewModel.CurrentTrolley, options);
            _logger.LogInformation("AddToTrolley completed");
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    [HttpGet]
    [CustomAuthorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveItem(string id)
    {
        try
        {
            _logger.LogInformation("RemoveItem called");
            await _marketPlaceViewModel.RemoveFromTrolley(id);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            string trolleyJson = JsonSerializer.Serialize(_marketPlaceViewModel.CurrentTrolley, options);
            _logger.LogInformation("RemoveItem completed");
            return Content(trolleyJson, "application/json");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return RedirectToAction("Error500", "Error");
        }
    }

    [HttpGet]
    [CustomAuthorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckOut(string trolleyid)
    {
        try
        {
            _logger.LogInformation("CheckOut called");
            await _marketPlaceViewModel.CheckOut(trolleyid);
            await _marketPlaceViewModel.GetTrolley();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            string trolleyJson = JsonSerializer.Serialize(_marketPlaceViewModel.CurrentTrolley, options);
            _logger.LogInformation("CheckOut completed");
            return Content(trolleyJson, "application/json");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return RedirectToAction("Error500", "Error");
        }
    }

    [HttpGet]
    [CustomAuthorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Trolley()
    {
        try
        {
            _logger.LogInformation("Trolley reload called");
            await _marketPlaceViewModel.GetTrolley();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            string trolleyJson = JsonSerializer.Serialize(_marketPlaceViewModel.CurrentTrolley, options);
            _logger.LogInformation("Trolley reload completed");
            return Content(trolleyJson, "application/json");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return RedirectToAction("Error500", "Error");
        }
    }

    [HttpGet]
    [CustomAuthorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FilterProducts([FromQuery] ProductSearchCommand command)
    {
        try
        {
            _logger.LogInformation("FilterProducts called");
            await _marketPlaceViewModel.UpdateCatalog(command.productName, command.categoryCheckbox, command.promotedCheckbox, command.sortBy);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            string filteredProductsJson = JsonSerializer.Serialize(_marketPlaceViewModel.Catalog, options);
            _logger.LogInformation("FilterProducts completed");
            return Content(filteredProductsJson, "application/json");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return RedirectToAction("Error500", "Error");
        }
    }

    [CustomAuthorize]
    public async Task<IActionResult> Detail(string id)
    {
        await _detailMarketProduct.FindProduct(id);
        return View(_detailMarketProduct);
    }
}