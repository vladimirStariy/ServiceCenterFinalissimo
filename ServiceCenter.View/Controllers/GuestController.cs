using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.View.Controllers
{
    public class GuestController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly ITariffService _tariffService;

        public GuestController(IServiceService serviceService, ITariffService tariffService)
        {
            _serviceService = serviceService;
            _tariffService = tariffService;

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
        public IActionResult Tariffs()
        {
            var response = _tariffService.GetTariffView();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Result);
            }
            return View("Error", $"{response.Description}");
        }
    }
}
