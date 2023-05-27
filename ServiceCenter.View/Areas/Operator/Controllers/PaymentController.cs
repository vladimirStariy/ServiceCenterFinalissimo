using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceCenter.Domain.Viewmodel.Payment;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.View.Areas.Operator.Controllers
{
    [Area("Operator")]
    [Authorize(Roles = "Operator")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IAbonentService _abonentService;

        public PaymentController(IPaymentService paymentService, IAbonentService abonentService)
        {
            _paymentService = paymentService;
            _abonentService = abonentService;
        }

        [HttpGet]
        public IActionResult Payments()
        {
            var response = _paymentService.GetPaymentView();
            ViewBag.DeleteError = false;
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Result);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult CreatePayment()
        {
            try
            {
                var response = _abonentService.Get();
                ViewBag.Abonents = new SelectList(response.Result, "Abonent_ID", "Name");
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
        public async Task<IActionResult> CreatePayment(PaymentViewModel model)
        {
            var response = await _paymentService.Create(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Payments", "Payment");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult UpdatePayment(uint id)
        {
            var response1 = _abonentService.Get();
            ViewBag.Abonents = new SelectList(response1.Result, "Abonent_ID", "Name");
            var response2 = _paymentService.GetById(id);
            if (response2.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response2.Result);
            }
            return View("Error", $"{response2.Description}");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePayment(PaymentViewModel model)
        {
            var response = await _paymentService.Update(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Payments", "Payment");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> DeletePayment(uint id)
        {
            try
            {
                await _paymentService.Remove(id);
                return RedirectToAction("Payments");
            }
            catch
            {
                ViewBag.DeleteError = true;
                return RedirectToAction("Payments");
            }
        }
    }
}
