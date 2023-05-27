using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Domain.Viewmodel.OrderServiceV;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.View.Areas.Operator.Controllers
{
    [Area("Operator")]
    [Authorize(Roles = "Operator")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public IActionResult Services()
        {
            var response = _serviceService.GetServiceView();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Result);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult CreateService() => View();

        [HttpPost]
        public async Task<IActionResult> CreateService(OrderServiceViewModel model)
        {
            var response = await _serviceService.Create(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Services", "Service");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult UpdateOrderService(uint id)
        {
            var response = _serviceService.GetByViewId(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Result);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderService(uint id, OrderServiceViewModel model)
        {
            model.OrderService_ID = id;
            var response = await _serviceService.Update(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Services", "Service");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteService(uint id)
        {
            await _serviceService.Remove(id);
            return RedirectToAction("Services");
        }


    }
}
