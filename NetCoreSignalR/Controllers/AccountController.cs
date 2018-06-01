using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace NetCoreSignalR.Controllers
{
    public class AccountController : Controller
    {
        public async Task<IActionResult> Login(string ReturnUrl)
        {


            var property = new AuthenticationProperties()
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddHours(2),
                IsPersistent = true,
            };
            ClaimsIdentity identity = new ClaimsIdentity("actionPermission");
            identity.AddClaim(new Claim(ClaimTypes.Email, "234123@qq.com"));
            identity.AddClaim(new Claim(ClaimTypes.Name, "Tim"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            ClaimsPrincipal user = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(user, property);



            //ClaimsIdentity identity = new ClaimsIdentity("Signalr");
            //identity.AddClaim(new Claim(ClaimTypes.Name, "tim"));
            //identity.AddClaim(new Claim(ClaimTypes.MobilePhone, "12346"));
            //identity.AddClaim(new Claim(ClaimTypes.Email, "12346@gmail.com"));
            //ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            //await HttpContext.SignInAsync(principal);
            return Content("success");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}