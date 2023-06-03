using ServiceCenter.Data.Repository;
using ServiceCenter.Data.Repository.Implementation;
using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Enum;
using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Viewmodel.Order;
using ServiceCenter.Domain.Viewmodel.TariffType;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.Service.Implementations
{
    public class OOrderService : IOrderService
    {
        private readonly IRepository<Order>  _orderRepository;

        public OOrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IBaseResponce<Order>> Create(OrderViewModel model)
        {
            try
            {
                var order = new Order();
                order.Status = model.Status;
                order.Order_date = model.Order_date;
                order.Order_close_date = model.Order_close_date;
                order.Abonent_ID = model.Abonent_ID;
                order.Employee_ID = model.Employee_ID;

                await _orderRepository.Create(order);

                return new BaseResponse<Order>()
                {
                    Result = order,
                    Description = "Объект добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Order>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public IBaseResponce<List<Order>> Get()
        {
            try
            {
                var materials = _orderRepository.Get().ToList();
                if (!materials.Any())
                {
                    return new BaseResponse<List<Order>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<Order>>()
                {
                    Result = materials,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Order>>()
                {
                    Description = $"[GetOrders] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<Order> GetById(uint id)
        {
            try
            {
                var order = _orderRepository.GetById(id).Result;
                if (order == null)
                {
                    return new BaseResponse<Order>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<Order>()
                {
                    Result = order,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Order>()
                {
                    Description = $"[GetOrder] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<List<OrderListViewModel>> GetOrderView()
        {
            try
            {
                var orders = _orderRepository.Get().ToList();
                var ordersView = new List<OrderListViewModel>();
                foreach (var item in orders)
                {
                    OrderListViewModel model = new OrderListViewModel();
                    model.Order_ID = item.Order_ID;
                    model.Order_date = item.Order_date;
                    if (item.Order_close_date != null)
                        model.Order_close_date = (DateTime)item.Order_close_date;
                    
                    model.Status = item.Status;
                    model.Abonent_Id = item.Abonent_ID;
                    model.Abonent_name = item.Abonent.Name;
                    model.Employee_name = item.Employee.Name;
                    ordersView.Add(model);
                }
                if (!ordersView.Any())
                {
                    return new BaseResponse<List<OrderListViewModel>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<OrderListViewModel>>()
                {
                    Result = ordersView,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<OrderListViewModel>>()
                {
                    Description = $"[GetOrders] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponce<Order>> Remove(uint id)
        {
            try
            {
                var model = _orderRepository.GetById(id).Result;
                if (model == null)
                {
                    return new BaseResponse<Order>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                await _orderRepository.Delete(model);
                return new BaseResponse<Order>()
                {
                    Result = model,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Order>()
                {
                    Description = $"[GetOrder] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponce<Order>> Update(OrderViewModel model)
        {
            try
            {
                var order = new Order();
                order.Order_ID = model._Order_ID;
                order.Order_date = model.Order_date;
                order.Order_close_date = model.Order_close_date;
                order.Abonent_ID = model.Abonent_ID;
                order.Employee_ID = model.Employee_ID;
                order.Status = model.Status;
                order.Services = model.Services;

                await _orderRepository.Update(order);

                var orderTemp = _orderRepository.GetById(model._Order_ID);

                return new BaseResponse<Order>()
                {
                    Result = order,
                    Description = "Объект изменен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Order>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<Order>> Update(Order entity)
        {
            try
            {
                await _orderRepository.Update(entity);

                return new BaseResponse<Order>()
                {
                    Result = entity,
                    Description = "Объект изменен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Order>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
