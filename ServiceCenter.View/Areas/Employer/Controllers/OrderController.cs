using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceCenter.Domain.Viewmodel.Order;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.View.Areas.Employer.Controllers
{
    [Area("Employer")]
    [Authorize(Roles = "Employee")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IAbonentService _abonentService;
        private readonly IEmployeeService _employeeService;
        public OrderController(IOrderService orderService, IAbonentService abonentService, IEmployeeService employeeService)
        {
            _orderService = orderService;
            _abonentService = abonentService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Orders()
        {
            var response = _orderService.GetOrderView();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Result);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult CreateOrder() => View();

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderViewModel model)
        {
            var response = await _orderService.Create(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Orders", "Order");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult UpdateOrder(uint id)
        {
            var response1 = _abonentService.Get();
            ViewBag.Abonents = new SelectList(response1.Result, "Abonent_ID", "Name");
            var response0 = _employeeService.Get();
            ViewBag.Employee = new SelectList(response0.Result, "Employee_ID", "Name");
            var response2 = _orderService.GetById(id);
            if (response2.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response2.Result);
            }
            return View("Error", $"{response2.Description}");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder(OrderViewModel model)
        {
            var response = await _orderService.Update(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Orders", "Order");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteOrder(uint id)
        {
            await _orderService.Remove(id);
            return RedirectToAction("Orders");
        }
    }
}
