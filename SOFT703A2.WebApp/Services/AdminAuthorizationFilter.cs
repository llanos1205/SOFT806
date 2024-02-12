using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SOFT703A2.WebApp.Services;

public class AdminAuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Check if the user is not in the "Admin" role
        if (!context.HttpContext.User.IsInRole("Admin"))
        {
            // Redirect to Error403 action in Error controller
            context.Result = new RedirectToActionResult("Error403", "Error", null);
        }
    }
}