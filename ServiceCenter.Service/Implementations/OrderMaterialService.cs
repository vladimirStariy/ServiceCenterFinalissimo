using ServiceCenter.Data.Repository;
using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Enum;
using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Viewmodel.OrderMaterialV;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.Service.Implementations
{
    public class OrderMaterialService : IOrderMaterial
    {
        private readonly IRepository<OrderMaterial> _orderMaterialRepository;

        public OrderMaterialService(IRepository<OrderMaterial> orderMaterialRepository)
        {
            _orderMaterialRepository = orderMaterialRepository;
        }

        public async Task<IBaseResponce<OrderMaterial>> Create(OrderMaterialViewModel model)
        {
            try
            {
                var orderMaterial = new OrderMaterial();
                orderMaterial.Material_ID = model.Material_ID;
                orderMaterial.Order_ID = model.Order_ID;
                orderMaterial.Count = model.Count;

                await _orderMaterialRepository.Create(orderMaterial);

                return new BaseResponse<OrderMaterial>()
                {
                    Result = orderMaterial,
                    Description = "Объект добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderMaterial>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public IBaseResponce<List<OrderMaterial>> Get()
        {
            try
            {
                var materials = _orderMaterialRepository.Get().ToList();
                if (!materials.Any())
                {
                    return new BaseResponse<List<OrderMaterial>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<OrderMaterial>>()
                {
                    Result = materials,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<OrderMaterial>>()
                {
                    Description = $"[GetOrderMaterials] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<List<OrderMaterial>> GetByOrderId(uint id)
        {
            try
            {
                var materials = _orderMaterialRepository.Get().Where(x => x.Order_ID == id).ToList();
                if (!materials.Any())
                {
                    return new BaseResponse<List<OrderMaterial>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<OrderMaterial>>()
                {
                    Result = materials,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<OrderMaterial>>()
                {
                    Description = $"[GetOrderMaterials] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<OrderMaterial> GetById(uint id)
        {
            try
            {
                var material = _orderMaterialRepository.GetById(id).Result;
                if (material == null)
                {
                    return new BaseResponse<OrderMaterial>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<OrderMaterial>()
                {
                    Result = material,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderMaterial>()
                {
                    Description = $"[GetOrderMaterial] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponce<OrderMaterial>> Remove(uint id)
        {
            try
            {
                var model = _orderMaterialRepository.GetById(id).Result;
                if (model == null)
                {
                    return new BaseResponse<OrderMaterial>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                await _orderMaterialRepository.Delete(model);
                return new BaseResponse<OrderMaterial>()
                {
                    Result = model,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderMaterial>()
                {
                    Description = $"[GetOrderMaterial] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponce<OrderMaterial>> Update(OrderMaterialViewModel model)
        {
            try
            {
                var orderMaterial = new OrderMaterial();
                orderMaterial.OrderMaterial_ID = model.id;
                orderMaterial.Order_ID = model.Order_ID;
                orderMaterial.Material_ID = model.Material_ID;
                orderMaterial.Count = model.Count;

                await _orderMaterialRepository.Update(orderMaterial);

                return new BaseResponse<OrderMaterial>()
                {
                    Result = orderMaterial,
                    Description = "Объект изменен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderMaterial>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    
    }
}
