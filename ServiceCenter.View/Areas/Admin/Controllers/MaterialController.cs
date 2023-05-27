using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Viewmodel.Abonent;
using ServiceCenter.Domain.Viewmodel.MaterialV;
using ServiceCenter.Service.Implementations;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.View.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MaterialController : Controller
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        public IActionResult Materials()
        {
                var response = _materialService.GetMaterialView();
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return View(response.Result);
                }
                return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult CreateMaterial() => View();

        [HttpPost]
        public async Task<IActionResult> CreateMaterial(MaterialViewModel model)
        {
            var response = await _materialService.Create(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Materials", "Material");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult UpdateMaterial(uint id)
        {
            var response = _materialService.GetById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Result);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMaterial(MaterialViewModel model)
        {
            var response = await _materialService.Update(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Materials", "Material");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteMaterial(uint id)
        {
            await _materialService.Remove(id);
            return RedirectToAction("Materials");
        }
    }
}
