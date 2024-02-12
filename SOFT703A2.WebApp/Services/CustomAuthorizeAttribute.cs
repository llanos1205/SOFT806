using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SOFT703A2.WebApp.Services;

public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            // User is not authenticated, redirect to a custom error page
            context.Result = new RedirectToActionResult("Error401", "Error", null);
        }
    }
}