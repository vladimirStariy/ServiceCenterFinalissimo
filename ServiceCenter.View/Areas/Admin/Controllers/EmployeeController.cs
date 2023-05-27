using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceCenter.Domain.Viewmodel.Employee;
using ServiceCenter.Domain.Viewmodel.OrderServiceV;
using ServiceCenter.Service.Implementations;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.View.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUserService _userService;

        public EmployeeController(IEmployeeService employeeService, IUserService userService)
        {
            _employeeService = employeeService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Employeers()
        {
            var response = _employeeService.GetEmployeeView();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Result);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult CreateEmployee()
        {
            var response = _userService.Get();
            ViewBag.Users = new SelectList(response.Result, "User_ID", "Username");
            return View();
        }
        

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeViewModel model)
        {
            var response = await _employeeService.Create(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Employeers", "Employee");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult UpdateEmployee(uint id)
        {
            var response = _userService.Get();
            ViewBag.Users = new SelectList(response.Result, "User_ID", "Username");
            var response2 = _employeeService.GetViewById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response2.Result);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(EmployeeViewModel model)
        {
            var response = await _employeeService.Update(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Employeers", "Employee");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEmployee(uint id)
        {
            await _employeeService.Remove(id);
            return RedirectToAction("Employeers");
        }
    }
}
