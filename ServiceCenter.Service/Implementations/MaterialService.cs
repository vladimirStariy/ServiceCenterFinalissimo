using ServiceCenter.Data.Repository;
using ServiceCenter.Data.Repository.Implementation;
using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Enum;
using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Viewmodel.Employee;
using ServiceCenter.Domain.Viewmodel.MaterialV;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.Service.Implementations
{
    public class MaterialService : IMaterialService
    {
        private readonly IRepository<Material> _materialRepository;

        public MaterialService(IRepository<Material> materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public async Task<IBaseResponce<Material>> Create(MaterialViewModel model)
        {
            try
            {
                var material = new Material();
                material.Name = model.Name;
                material.Count = model.Count;
                material.Price = model.Price;

                await _materialRepository.Create(material);

                return new BaseResponse<Material>()
                {
                    Result = material,
                    Description = "Объект добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Material>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public IBaseResponce<List<Material>> Get()
        {
            try
            {
                var materials = _materialRepository.Get().ToList();
                if (!materials.Any())
                {
                    return new BaseResponse<List<Material>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<Material>>()
                {
                    Result = materials,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Material>>()
                {
                    Description = $"[GetMaterials] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<Material> GetById(uint id)
        {
            try
            {
                var material = _materialRepository.GetById(id).Result;
                if (material == null)
                {
                    return new BaseResponse<Material>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<Material>()
                {
                    Result = material,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Material>()
                {
                    Description = $"[GetMaterial] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<List<MaterialListViewModel>> GetMaterialView()
        {
            try
            {
                var materials = _materialRepository.Get().ToList();
                var materialView = new List<MaterialListViewModel>();
                foreach (var item in materials)
                {
                    MaterialListViewModel model = new MaterialListViewModel();
                    model.Material_ID = item.Material_ID;
                    model.Name = item.Name;
                    model.Count = item.Count;
                    model.Price = item.Price;
                    materialView.Add(model);
                }
                if (!materialView.Any())
                {
                    return new BaseResponse<List<MaterialListViewModel>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<MaterialListViewModel>>()
                {
                    Result = materialView,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<MaterialListViewModel>>()
                {
                    Description = $"[GetMaterials] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<Material> GetViewById(uint id)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponce<Material>> Remove(uint id)
        {
            try
            {
                var model = _materialRepository.GetById(id).Result;
                if (model == null)
                {
                    return new BaseResponse<Material>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                await _materialRepository.Delete(model);
                return new BaseResponse<Material>()
                {
                    Result = model,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Material>()
                {
                    Description = $"[GetMaterial] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponce<Material>> Update(MaterialViewModel model)
        {
            try
            {
                var material = new Material();
                material.Material_ID = model.Material_ID;
                material.Name = model.Name;
                material.Count = model.Count;
                material.Price = model.Price;

                await _materialRepository.Update(material);

                return new BaseResponse<Material>()
                {
                    Result = material,
                    Description = "Объект изменен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Material>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
