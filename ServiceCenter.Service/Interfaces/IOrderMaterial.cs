using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Viewmodel.OrderMaterialV;

namespace ServiceCenter.Service.Interfaces
{
    public interface IOrderMaterial
    {
        Task<IBaseResponce<OrderMaterial>> Create(OrderMaterialViewModel model);
        Task<IBaseResponce<OrderMaterial>> Update(OrderMaterialViewModel model);
        Task<IBaseResponce<OrderMaterial>> Remove(uint id);

        IBaseResponce<List<OrderMaterial>> Get();
        IBaseResponce<List<OrderMaterial>> GetByOrderId(uint id);
        IBaseResponce<OrderMaterial> GetById(uint id);
    }
}
