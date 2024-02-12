using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Product;
using SOFT703A2.Infrastructure.RequestModels;
using SOFT703A2.Infrastructure.ViewModels.Product;
using SOFT703A2.WebApp.Services;

namespace SOFT703A2.WebApp.Controllers;

[ServiceFilter(typeof(AdminAuthorizationFilter))]
public class ProductController : Controller
{
    private readonly IListProductViewModel _listProductViewModel;
    private readonly ICreateProductViewModel _createProductViewModel;
    private readonly IDetailProductViewModel _detailProductViewModel;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IListProductViewModel listProductViewModel, ICreateProductViewModel createProductViewModel,
        IDetailProductViewModel detailProductViewModel, ILogger<ProductController> logger)
    {
        _listProductViewModel = listProductViewModel;
        _createProductViewModel = createProductViewModel;
        _detailProductViewModel = detailProductViewModel;
        _logger = logger;
    }

    public async Task<IActionResult> List()
    {
        try
        {
            _logger.LogInformation("List called");
            await _listProductViewModel.GetAll();
            return View(_listProductViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return RedirectToAction("Error500", "Error");
        }
    }

    public async Task<IActionResult> Detail(string id)
    {
        try
        {
            _logger.LogInformation("Detail called");
            await _detailProductViewModel.Find(id);
            await _detailProductViewModel.LoadCategories();
            return View(_detailProductViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return RedirectToAction("Error500", "Error");
        }
    }

    public async Task<IActionResult> Update(string id, DetailProductViewModel vm)
    {
        try
        {
            _logger.LogInformation("Update called");
            if (ModelState.IsValid)
            {
                _detailProductViewModel.Name = vm.Name;
                _detailProductViewModel.Photo = vm.Photo;
                _detailProductViewModel.Price = vm.Price;
                _detailProductViewModel.Stock = vm.Stock;
                _detailProductViewModel.IsPromoted = vm.IsPromoted;
                _detailProductViewModel.SelectedCategory = vm.SelectedCategory;
                var result = await _detailProductViewModel.Update(id);
                if (result)
                {
                    _logger.LogInformation("Update completed");
                    return RedirectToAction("List");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong");
                }
            }

            await _detailProductViewModel.LoadCategories();
            return View("Detail", _detailProductViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return RedirectToAction("Error500", "Error");
        }
    }

    public async Task<IActionResult> Add()
    {
        try
        {
            _logger.LogInformation("Add called");
            await _createProductViewModel.LoadCategories();
            return View(_createProductViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return RedirectToAction("Error500", "Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateProductViewModel vm)
    {
        try
        {
            _logger.LogInformation("Add called");
            if (ModelState.IsValid)
            {
                _createProductViewModel.Name = vm.Name;
                _createProductViewModel.Photo = vm.Photo;
                _createProductViewModel.Price = vm.Price;
                _createProductViewModel.Stock = vm.Stock;
                _createProductViewModel.SelectedCategory = vm.SelectedCategory;
                _createProductViewModel.IsPromoted = vm.IsPromoted;
                var result = await _createProductViewModel.Create();
                if (result)
                {
                    _logger.LogInformation("Add completed");
                    return RedirectToAction("List");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong");
                }
            }

            await _createProductViewModel.LoadCategories();
            return View(_createProductViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return RedirectToAction("Error500", "Error");
        }
    }


    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            _logger.LogInformation("Delete called");
            var result = await _createProductViewModel.Delete(id);
            if (result)
            {
                _logger.LogInformation("Delete completed");
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            return RedirectToAction("List");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return RedirectToAction("Error500", "Error");
        }
    }

    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> Promote([FromBody] IdModel model)
    {
        try
        {
            _logger.LogInformation("Promote called");
            await _detailProductViewModel.Promote(model.Id);
            await _listProductViewModel.GetAll();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            string productsJson = JsonSerializer.Serialize(_listProductViewModel.Products, options);
            return Content(productsJson, "application/json");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound();
        }
    }

    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> UnPromote([FromBody] IdModel model)
    {
        try
        {
            _logger.LogInformation("UnPromote called");
            await _detailProductViewModel.UnPromote(model.Id);
            await _listProductViewModel.GetAll();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            string productsJson = JsonSerializer.Serialize(_listProductViewModel.Products, options);
            return Content(productsJson, "application/json");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound();
        }
    }
}