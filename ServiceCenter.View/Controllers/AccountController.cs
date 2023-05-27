using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Domain.Viewmodel.User;
using ServiceCenter.Service.Interfaces;
using System.Security.Claims;

namespace ServiceCenter.View.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Login(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Result));
                }

                if (User.IsInRole("Admin"))
                    return RedirectToAction("Tariffs", "Tariff", new { area = "Admin" });
                else
                if (User.IsInRole("Operator"))
                    return RedirectToAction("Services", "Service", new { area = "Operator" });
                else
                    return RedirectToAction("Tariffs", "Guest");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Services", "Guest", new { area = "" });
        }
    }
}
