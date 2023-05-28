using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Viewmodel.Order;
using ServiceCenter.Domain.Viewmodel.OrderMaterialV;
using ServiceCenter.Domain.Viewmodel.OrderServiceV;
using ServiceCenter.Service.Implementations;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.View.Areas.Operator.Controllers
{
    [Area("Operator")]
    [Authorize(Roles = "Operator")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IAbonentService _abonentService;
        private readonly IEmployeeService _employeeService;
        private readonly IMaterialService _materialService;
        private readonly IServiceService _serviceService;
        private readonly IOrderMaterial _orderMaterialService;

        public OrderController(IOrderService orderService, IAbonentService abonentService, IEmployeeService employeeService, IMaterialService materialService, IServiceService serviceService, IOrderMaterial orderMaterialService)
        {
            _orderService = orderService;
            _abonentService = abonentService;
            _employeeService = employeeService;
            _materialService = materialService;
            _serviceService = serviceService;
            _orderMaterialService = orderMaterialService;
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
        public IActionResult AbonentDetails(uint id)
        {
            var abonent = _abonentService.GetById(id);
            return View(abonent.Result);
        }

        [HttpGet]
        public IActionResult OrderForm(uint id)
        {
            var response = _orderService.GetById(id);
            var order = response.Result;
            OrderFormViewModel OFVM = new OrderFormViewModel();
            OFVM.Order_date = order.Order_date;
            OFVM.Services = order.Services.ToList();
            OFVM.Status = order.Status;
            OFVM.Abonent_ID = order.Abonent_ID;
            OFVM.Employee_ID = order.Employee_ID;
            OFVM.Order_close_date = order.Order_close_date;
            OFVM.id = id;

            var response2 = _orderMaterialService.GetByOrderId(id);
            OFVM.Materials = response2.Result;

            var response3 = _abonentService.Get();
            ViewBag.Abonents = new SelectList(response3.Result, "Abonent_ID", "Name");

            var response4 = _employeeService.Get();
            ViewBag.Employers = new SelectList(response4.Result, "Employee_ID", "Name");

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(OFVM);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult OrderServiceAdd(uint id)
        {
            var response3 = _serviceService.Get();
            ViewBag.Services = new SelectList(response3.Result, "OrderService_ID", "Name");
            HttpContext.Session.SetInt32("_orderid", (int)id);

            return View();
        }

        [HttpPost]
        public IActionResult OrderServiceAdd(OrderServiceViewModel model)
        {
            var order_id = HttpContext.Session.GetInt32("_orderid").ToString();
            var Order_ID = Convert.ToUInt32(order_id);
            var order = _orderService.GetById(Order_ID).Result;
            var service = _serviceService.GetById(model.OrderService_ID).Result;
            order.Services.Add(service);

            OrderViewModel OVM = new OrderViewModel();
            OVM.Order_date = order.Order_date;
            OVM.Status = order.Status;
            OVM.Order_ID = order.Order_ID;
            OVM.Abonent_ID = order.Abonent_ID;
            OVM.Employee_ID = order.Employee_ID;
            OVM.Order_close_date = order.Order_close_date;
            OVM.Services = order.Services;
            var response = _orderService.Update(OVM);

            return RedirectToAction("OrderForm", "Order", new {id = Order_ID });
        }

        [HttpPost]
        public async Task<IActionResult> OrderFormAsync(OrderFormViewModel model)
        {
            OrderViewModel OVM = new OrderViewModel();
            OVM.Order_date = model.Order_date;
            OVM.Status = model.Status;
            OVM.Order_ID = model.id;
            OVM.Abonent_ID = model.Abonent_ID;
            OVM.Employee_ID = model.Employee_ID;
            OVM.Order_close_date = model.Order_close_date;

            var response = await _orderService.Update(OVM);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Orders", "Order");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult AddOrderMaterial(uint id)
        {
            try
            {
                var response1 = _materialService.Get();
                ViewBag.Materials = new SelectList(response1.Result, "Material_ID", "Name");
                HttpContext.Session.SetInt32("_orderid", (int)id);

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
        public async Task<IActionResult> AddOrderMaterial(OrderMaterialViewModel model)
        {
            var order_id = HttpContext.Session.GetInt32("_orderid").ToString();
            model.Order_ID = Convert.ToUInt32(order_id);
            var response = await _orderMaterialService.Create(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("OrderForm", "Order", new { id = model.Order_ID });
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult UpdateOrderMaterial(uint id)
        {
            var response1 = _materialService.Get();
            ViewBag.Materials = new SelectList(response1.Result, "Material_ID", "Name");
            var response2 = _orderMaterialService.GetById(id);
            var orderMaterial = response2.Result;
            OrderMaterialViewModel OMVM = new OrderMaterialViewModel();
            OMVM.Material_ID = orderMaterial.Material_ID;
            OMVM.Count = orderMaterial.Count;
            HttpContext.Session.SetInt32("_orderid", (int)orderMaterial.Order_ID);
            OMVM.id = orderMaterial.OrderMaterial_ID;
            return View(OMVM);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderMaterial(uint Order_ID, OrderMaterialViewModel model)
        {
            var order_id = HttpContext.Session.GetInt32("_orderid").ToString();
            model.Order_ID = Convert.ToUInt32(order_id);
            var response = await _orderMaterialService.Update(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("OrderForm", "Order", new { id = model.Order_ID });
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteOrderMaterial(uint id)
        {
            var ordermaterial = _orderMaterialService.GetById(id);
            var order_id = ordermaterial.Result.Order_ID;
            await _orderMaterialService.Remove(id);
            return RedirectToAction("OrderForm", "Order", new { id = order_id });
        }

        [HttpGet]
        public IActionResult CreateOrder() 
        {
            try
            {
                var response1 = _abonentService.Get();
                ViewBag.Abonents = new SelectList(response1.Result, "Abonent_ID", "Name");
                var response2 = _employeeService.Get();
                ViewBag.Employeers = new SelectList(response2.Result, "Employee_ID", "Name");
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
