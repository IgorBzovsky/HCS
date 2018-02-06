using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace HCS.Client.Components
{
    public class AppSettingsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var token = await AuthenticationHttpContextExtensions.GetTokenAsync(HttpContext, "access_token");
            var id_token = await AuthenticationHttpContextExtensions.GetTokenAsync(HttpContext, "id_token");
            var refresh_token = await AuthenticationHttpContextExtensions.GetTokenAsync(HttpContext, "refresh_token");

            var settings = new AppSettingsModel
            {
                WebUrl = "http://localhost:5000",
                ApiUrl = "http://localhost:5001",
                AuthUrl = "http://localhost:5002",
                RedirectUrl = "/home",
                AccessToken = token,
                UserFullName = HttpContext.User.FindFirstValue("name"),
                UserEmail = HttpContext.User.FindFirstValue("email"),
                AdminEnabled = HttpContext.User.HasClaim("role", "admin")
            };
            return View(settings);
        }
    }
}
