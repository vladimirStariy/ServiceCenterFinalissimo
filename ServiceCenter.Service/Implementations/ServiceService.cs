using ServiceCenter.Data.Repository;
using ServiceCenter.Data.Repository.Implementation;
using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Enum;
using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Viewmodel.OrderServiceV;
using ServiceCenter.Domain.Viewmodel.Tariff;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.Service.Implementations
{
    public class ServiceService : IServiceService
    {
        private readonly IRepository<OrderService> _serviceRepository;

        public ServiceService(IRepository<OrderService> serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<IBaseResponce<OrderService>> Create(OrderServiceViewModel model)
        {
            try
            {
                var service = new OrderService();
                service.Name = model.Name;
                service.Description = model.Description;
                service.Price = model.Price;

                await _serviceRepository.Create(service);

                return new BaseResponse<OrderService>()
                {
                    Result = service,
                    Description = "Объект добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderService>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public IBaseResponce<List<OrderService>> Get()
        {
            try
            {
                var services = _serviceRepository.Get().ToList();
                if (!services.Any())
                {
                    return new BaseResponse<List<OrderService>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<OrderService>>()
                {
                    Result = services,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<OrderService>>()
                {
                    Description = $"[GetServices] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<OrderService> GetById(uint id)
        {
            try
            {
                var service = _serviceRepository.GetById(id).Result;
                if (service == null)
                {
                    return new BaseResponse<OrderService>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<OrderService>()
                {
                    Result = service,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderService>()
                {
                    Description = $"[GetService] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<OrderServiceViewModel> GetByViewId(uint id)
        {
            try
            {
                var service = _serviceRepository.GetById(id).Result;
                
                if (service == null)
                {
                    return new BaseResponse<OrderServiceViewModel>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }

                var serviceView = new OrderServiceViewModel();
                serviceView.OrderService_ID = service.OrderService_ID;
                serviceView.Name = service.Name;
                serviceView.Price = service.Price;
                serviceView.Description = service.Description;

                return new BaseResponse<OrderServiceViewModel>()
                {
                    Result = serviceView,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderServiceViewModel>()
                {
                    Description = $"[GetService] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<List<OrderServiceListViewModel>> GetServiceView()
        {
            try
            {
                var services = _serviceRepository.Get().ToList();
                var serviceView = new List<OrderServiceListViewModel>();
                foreach (var item in services)
                {
                    OrderServiceListViewModel model = new OrderServiceListViewModel();

                    model.OrderService_ID = item.OrderService_ID;
                    model.Name = item.Name;
                    model.Price = item.Price;
                    model.Description = item.Description;
                    serviceView.Add(model);
                }
                if (!serviceView.Any())
                {
                    return new BaseResponse<List<OrderServiceListViewModel>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<OrderServiceListViewModel>>()
                {
                    Result = serviceView,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<OrderServiceListViewModel>>()
                {
                    Description = $"[GetService] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponce<OrderService>> Remove(uint id)
        {
            try
            {
                var model = _serviceRepository.GetById(id).Result;
                if (model == null)
                {
                    return new BaseResponse<OrderService>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                await _serviceRepository.Delete(model);
                return new BaseResponse<OrderService>()
                {
                    Result = model,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderService>()
                {
                    Description = $"[GetOrderService] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponce<OrderService>> Update(OrderServiceViewModel model)
        {
            try
            {
                var service = new OrderService();
                service.OrderService_ID = model.OrderService_ID;
                service.Name = model.Name;
                service.Description = model.Description;
                service.Price = model.Price;

                await _serviceRepository.Update(service);

                return new BaseResponse<OrderService>()
                {
                    Result = service,
                    Description = "Объект изменен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderService>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
