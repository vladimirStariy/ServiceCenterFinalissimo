using ServiceCenter.Data.Repository;
using ServiceCenter.Data.Repository.Implementation;
using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Enum;
using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Viewmodel.Tariff;
using ServiceCenter.Domain.Viewmodel.TariffType;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.Service.Implementations
{
    public class TariffService : ITariffService
    {
        private readonly IRepository<Tariff> _tariffRepository;
        private readonly IRepository<TariffType> _tariffTypeRepository;
        public TariffService(IRepository<Tariff> tariffRepository, IRepository<TariffType> tariffTypeRepository)
        {
            _tariffRepository = tariffRepository;
            _tariffTypeRepository = tariffTypeRepository;

        }

        public async Task<IBaseResponce<Tariff>> Create(TariffViewModel model)
        {
            try
            {
                var tariff = new Tariff();
                tariff.Tariff_ID = model.Id;
                tariff.Name = model.Name;
                tariff.Price = model.Price;
                tariff.Description = model.Description;
                tariff.TariffType_ID = model.TariffType;

                await _tariffRepository.Create(tariff);

                return new BaseResponse<Tariff>()
                {
                    Result = tariff,
                    Description = "Объект добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Tariff>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public IBaseResponce<List<Tariff>> Get()
        {
            try
            {
                var tariffs = _tariffRepository.Get().ToList();
                if (!tariffs.Any())
                {
                    return new BaseResponse<List<Tariff>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<Tariff>>()
                {
                    Result = tariffs,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Tariff>>()
                {
                    Description = $"[GetTariffs] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<Tariff> GetById(uint id)
        {
            try
            {
                var tariff = _tariffRepository.GetById(id).Result;
                if (tariff == null)
                {
                    return new BaseResponse<Tariff>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<Tariff>()
                {
                    Result = tariff,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Tariff>()
                {
                    Description = $"[GetTariff] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<List<TariffListViewModel>> GetTariffView()
        {
            try
            {
                var tariffTypes = _tariffRepository.Get().ToList();
                var tariffView = new List<TariffListViewModel>();
                foreach (var item in tariffTypes)
                {
                    TariffListViewModel model = new TariffListViewModel();

                    model.Id = (int)item.Tariff_ID;
                    model.Name = item.Name;
                    model.Price = item.Price;
                    model.TariffType = _tariffTypeRepository.Get().Where(x => x.TariffType_ID == item.TariffType_ID).Select(x => x.Name).FirstOrDefault().ToString();
                    tariffView.Add(model);
                }
                if (!tariffView.Any())
                {
                    return new BaseResponse<List<TariffListViewModel>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<TariffListViewModel>>()
                {
                    Result = tariffView,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<TariffListViewModel>>()
                {
                    Description = $"[GetTariff] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponce<Tariff>> Remove(uint id)
        {
            try
            {
                var model = _tariffRepository.GetById(id).Result;
                if (model == null)
                {
                    return new BaseResponse<Tariff>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                await _tariffRepository.Delete(model);
                return new BaseResponse<Tariff>()
                {
                    Result = model,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Tariff>()
                {
                    Description = $"[GetTariff] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponce<Tariff>> Update(TariffViewModel model)
        {
            try
            {
                var tariff = new Tariff();
                tariff.Tariff_ID = model.TariffType;
                tariff.Tariff_ID = model.Id;
                tariff.Name = model.Name;
                tariff.Price = model.Price;
                tariff.Description = model.Description;
                tariff.TariffType_ID = model.TariffType;

                await _tariffRepository.Update(tariff);

                return new BaseResponse<Tariff>()
                {
                    Result = tariff,
                    Description = "Объект изменен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Tariff>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
