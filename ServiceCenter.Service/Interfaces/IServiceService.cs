using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Entity;
using ServiceCenter.Service.Implementations;
using ServiceCenter.Domain.Viewmodel.OrderServiceV;

namespace ServiceCenter.Service.Interfaces
{
    public interface IServiceService
    {
        Task<IBaseResponce<OrderService>> Create(OrderServiceViewModel model);
        Task<IBaseResponce<OrderService>> Update(OrderServiceViewModel model);
        Task<IBaseResponce<OrderService>> Remove(uint id);

        IBaseResponce<List<OrderService>> Get();
        IBaseResponce<List<OrderServiceListViewModel>> GetServiceView();
        IBaseResponce<OrderService> GetById(uint id);
        IBaseResponce<OrderServiceViewModel> GetByViewId(uint id);
    }
}
