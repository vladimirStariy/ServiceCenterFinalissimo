using ServiceCenter.Data.Repository;
using ServiceCenter.Data.Repository.Implementation;
using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Enum;
using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Viewmodel.TariffType;
using ServiceCenter.Domain.Viewmodel.User;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.Service.Implementations
{
    public class TariffTypeService : ITariffTypeService
    {
        private readonly IRepository<TariffType> _tariffTypeRepository;

        public TariffTypeService(IRepository<TariffType> tariffTypeRepository)
        {
            _tariffTypeRepository = tariffTypeRepository;
        }

        public async Task<IBaseResponce<TariffType>> Create(TariffTypeViewModel model)
        {
            try
            {
                var tariffType = new TariffType()
                {
                    Name = model.Name
                };

                await _tariffTypeRepository.Create(tariffType);

                return new BaseResponse<TariffType>()
                {
                    Result = tariffType,
                    Description = "Тип тарифа добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TariffType>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public IBaseResponce<List<TariffType>> Get()
        {
            try
            {
                var tariffTypes = _tariffTypeRepository.Get().ToList();
                if (!tariffTypes.Any())
                {
                    return new BaseResponse<List<TariffType>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<TariffType>>()
                {
                    Result = tariffTypes,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<TariffType>>()
                {
                    Description = $"[GetTariffTypes] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<TariffType> GetById(uint id)
        {
            try
            {
                var tariffType = _tariffTypeRepository.GetById(id).Result;
                if (tariffType == null)
                {
                    return new BaseResponse<TariffType>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<TariffType>()
                {
                    Result = tariffType,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TariffType>()
                {
                    Description = $"[GetTariffType] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<List<TariffTypeListViewModel>> GetTarifTypeView()
        {
            try
            {
                var tariffTypes = _tariffTypeRepository.Get().ToList();
                var tariffTypesView = new List<TariffTypeListViewModel>();
                foreach (var item in tariffTypes)
                {
                    TariffTypeListViewModel model = new TariffTypeListViewModel();
                    model.Id = item.TariffType_ID;
                    model.Name = item.Name;
                    tariffTypesView.Add(model);
                }
                if (!tariffTypesView.Any())
                {
                    return new BaseResponse<List<TariffTypeListViewModel>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<TariffTypeListViewModel>>()
                {
                    Result = tariffTypesView,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<TariffTypeListViewModel>>()
                {
                    Description = $"[GetTariffTypes] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponce<TariffType>> Remove(uint id)
        {
            try
            {
                var model = _tariffTypeRepository.GetById(id).Result;
                if (model == null)
                {
                    return new BaseResponse<TariffType>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                await _tariffTypeRepository.Delete(model);
                return new BaseResponse<TariffType>()
                {
                    Result = model,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TariffType>()
                {
                    Description = $"[GetTariff] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponce<TariffType>> Update(TariffTypeViewModel model)
        {
            try
            {
                var tariffType = new TariffType()
                {
                    TariffType_ID = model.Id,
                    Name = model.Name
                };

                await _tariffTypeRepository.Update(tariffType);

                return new BaseResponse<TariffType>()
                {
                    Result = tariffType,
                    Description = "Тип тарифа обновлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TariffType>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
