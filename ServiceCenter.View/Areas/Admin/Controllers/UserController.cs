using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceCenter.Domain.Viewmodel.User;
using ServiceCenter.Service.Implementations;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.View.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private class roles
        {
            public roles(string name, int id)
            {
                Name = name;
                Id = id;
            }
            public string Name { get; set; }
            public int Id { get; set; }
        }

        [HttpGet]
        public IActionResult Users()
        {
            var response = _userService.GetUserView();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View("users", response.Result);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            List<roles> roles = new List<roles>();
            roles.Add(new roles("Admin", 0));
            roles.Add(new roles("Employee", 1));
            roles.Add(new roles("Operator", 2));
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserViewModel model)
        {
            var response = await _userService.Create(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Users", "User");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult UpdateUser(uint id)
        {
            List<roles> roles = new List<roles>();
            roles.Add(new roles("Admin", 0));
            roles.Add(new roles("Employee", 1));
            roles.Add(new roles("Operator", 2));
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            var response2 = _userService.GetById(id);
            UserViewModel UVM = new UserViewModel();
            UVM.User_ID = response2.Result.User_ID;
            UVM.Login = response2.Result.Username;
            UVM.Password = response2.Result.Password;
            UVM.Role = (int)response2.Result.Role;
            if (response2.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(UVM);
            }
            return View("Error", $"{response2.Description}");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserViewModel model)
        {
            var response = await _userService.Update(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Users", "User");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(uint id)
        {
            await _userService.Remove(id);
            return RedirectToAction("Users", "User");
        }
    }
}
