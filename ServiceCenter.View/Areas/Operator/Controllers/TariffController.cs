using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceCenter.Domain.Viewmodel.Tariff;
using ServiceCenter.Domain.Viewmodel.TariffType;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.View.Areas.Operator.Controllers
{
    [Area("Operator")]
    [Authorize(Roles = "Operator")]
    public class TariffController : Controller
    {
        private readonly ITariffTypeService _tariffTypeService;
        private readonly ITariffService _tariffService;

        public TariffController(ITariffTypeService tariffTypeService, ITariffService tariffService)
        {
            _tariffTypeService = tariffTypeService;
            _tariffService = tariffService;
        }

        [HttpGet]
        public IActionResult TariffTypes()
        {
            var response = _tariffTypeService.GetTarifTypeView();
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
            ViewBag.DeleteError = false;
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Result);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult CreateTariffType() => View();

        [HttpPost]
        public async Task<IActionResult> CreateTariffType(TariffTypeViewModel model)
        {
            var response = await _tariffTypeService.Create(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("TariffTypes", "Tariff");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult CreateTariff()
        {
            try
            {
                var response = _tariffTypeService.Get();
                ViewBag.TariffTypesCmb = new SelectList(response.Result, "TariffType_ID", "Name");
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
        public async Task<IActionResult> CreateTariff(TariffViewModel model)
        {
            var response = await _tariffService.Create(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Tariffs", "Tariff");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult UpdateTariffType(uint id)
        {
            var response = _tariffTypeService.GetById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Result);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTariffType(TariffTypeViewModel model)
        {
            var response = await _tariffTypeService.Update(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("TariffTypes", "Tariff");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult UpdateTariff(uint id)
        {
            var res = _tariffTypeService.Get();
            ViewBag.TariffTypes = new SelectList(res.Result, "TariffType_ID", "Name");
            var response = _tariffService.GetById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Result);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTariff(TariffViewModel model)
        {

            var response = await _tariffService.Update(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Tariffs", "Tariff");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTariff(uint id)
        {
            try
            {
                await _tariffService.Remove(id);
                return RedirectToAction("Tariffs");
            }
            catch
            {
                ViewBag.DeleteError = true;
                return RedirectToAction("Tariffs");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTariffType(uint id)
        {
            await _tariffTypeService.Remove(id);
            return RedirectToAction("TariffTypes");
        }
    }
}
