using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Viewmodel.MaterialV;
using ServiceCenter.Domain.Viewmodel.Order;

namespace ServiceCenter.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IBaseResponce<Order>> Create(OrderViewModel model);
        Task<IBaseResponce<Order>> Update(OrderViewModel model);
        Task<IBaseResponce<Order>> Remove(uint id);
        Task<IBaseResponce<Order>> Update(Order entity);
        IBaseResponce<List<Order>> Get();
        IBaseResponce<List<OrderListViewModel>> GetOrderView();
        IBaseResponce<Order> GetById(uint id);

    }
}
