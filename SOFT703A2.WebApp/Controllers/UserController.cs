using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOFT703A2.Infrastructure.Commands;
using SOFT703A2.Infrastructure.Contracts.ViewModels.User;
using SOFT703A2.Infrastructure.ViewModels.User;
using SOFT703A2.WebApp.Services;

namespace SOFT703A2.WebApp.Controllers;

[ServiceFilter(typeof(AdminAuthorizationFilter))]
public class UserController : Controller
{
    private readonly IListUserViewModel _listUserViewModel;
    private readonly IDetailUserViewModel _detailUserViewModel;
    private readonly ICreateUserViewModel _createUserViewModel;
    private readonly ILogger<UserController> _logger;

    public UserController(IListUserViewModel listUserViewModel, IDetailUserViewModel detailUserViewModel,
        ICreateUserViewModel createUserViewModel, ILogger<UserController> logger)
    {
        _listUserViewModel = listUserViewModel;
        _detailUserViewModel = detailUserViewModel;
        _createUserViewModel = createUserViewModel;
        _logger = logger;
    }

    public async Task<IActionResult> List()
    {
        try
        {
            _logger.LogInformation("List called");
            await _listUserViewModel.GetAllAsync();
            return View(_listUserViewModel);
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
            await _detailUserViewModel.Find(id);
            await _detailUserViewModel.LoadRoles();
            return View(_detailUserViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return RedirectToAction("Error500", "Error");
        }
    }

    public async Task<IActionResult> Update(string id, DetailUserViewModel vm)
    {
        try
        {
            _logger.LogInformation("Update called");
            if (ModelState.IsValid)
            {
                _detailUserViewModel.FirstName = vm.FirstName;
                _detailUserViewModel.LastName = vm.LastName;
                _detailUserViewModel.PhoneNumber = vm.PhoneNumber;
                _detailUserViewModel.Email = vm.Email;
                _detailUserViewModel.Id = vm.Id;
                _detailUserViewModel.SelectedRole = vm.SelectedRole;
                var result = await _detailUserViewModel.Update();
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

            await _detailUserViewModel.LoadRoles();
            return View("Detail", _detailUserViewModel);
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
            await _createUserViewModel.LoadRoles();
            return View(_createUserViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return RedirectToAction("Error500", "Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateUserViewModel vm)
    {
        try
        {
            _logger.LogInformation("Add called");
            if (ModelState.IsValid)
            {
                _createUserViewModel.FirstName = vm.FirstName;
                _createUserViewModel.LastName = vm.LastName;
                _createUserViewModel.PhoneNumber = vm.PhoneNumber;
                _createUserViewModel.Email = vm.Email;
                _createUserViewModel.SelectedRole = vm.SelectedRole;
                await _createUserViewModel.Create();
                _logger.LogInformation("Add completed");
                return RedirectToAction("List");
            }

            await _createUserViewModel.LoadRoles();
            return View(_createUserViewModel);
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
            var result = await _createUserViewModel.Delete(id);
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

    [HttpGet]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FilterUsers([FromQuery] UserSearchCommand command)
    {
        try
        {
            _logger.LogInformation("FilterUsers called");
            await _listUserViewModel.UpdateUsersList(command.userName, command.visitsCheckbox, command.emailCheckbox,
                command.phoneCheckbox, command.sortBy);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            string filteredProductsJson = JsonSerializer.Serialize(_listUserViewModel.Users, options);
            _logger.LogInformation("FilterUsers completed");
            return Content(filteredProductsJson, "application/json");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return RedirectToAction("Error500", "Error");
        }
    }
}