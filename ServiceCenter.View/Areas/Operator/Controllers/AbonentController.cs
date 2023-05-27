using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceCenter.Domain.Viewmodel.Abonent;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.View.Areas.Operator.Controllers
{
    [Area("Operator")]
    [Authorize(Roles = "Operator")]
    public class AbonentController : Controller
    {
        private readonly IAbonentService _abonentService;
        private readonly ITariffService _tariffService;

        public AbonentController(IAbonentService abonentService, ITariffService tariffService)
        {
            _abonentService = abonentService;
            _tariffService = tariffService;
        }

        [HttpGet]
        public IActionResult Abonents()
        {
            var response = _abonentService.GetAbonentView();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Result);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult AbonentAdd()
        {
            try
            {
                var response = _tariffService.Get();
                ViewBag.Tariff = new SelectList(response.Result, "Tariff_ID", "Name");
                ViewBag.Error = false;
                return View();
            }
            catch
            {
                ViewBag.Error = true;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AbonentAdd(AbonentViewModel model)
        {
            var response = await _abonentService.Create(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Abonents", "Abonent");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult UpdateAbonent(uint id)
        {
            var response = _tariffService.Get();
            ViewBag.Tariff = new SelectList(response.Result, "Tariff_ID", "Name");
            var response2 = _abonentService.GetById(id);
            return View(response2.Result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbonent(uint id, AbonentViewModel model)
        {
            model.Abonent_ID = id;
            var response = await _abonentService.Update(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Abonents", "Abonent");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAbonent(uint id)
        {
            await _abonentService.Remove(id);
            return RedirectToAction("Abonents");
        }
    }
}
