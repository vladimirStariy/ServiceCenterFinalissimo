using ServiceCenter.Data.Repository;
using ServiceCenter.Data.Repository.Implementation;
using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Enum;
using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Viewmodel.Abonent;
using ServiceCenter.Domain.Viewmodel.Order;
using ServiceCenter.Domain.Viewmodel.Tariff;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.Service.Implementations
{
    public class AbonentService : IAbonentService
    {
        private readonly IRepository<Abonent> _abonentRepository;
        private readonly IRepository<Tariff> _tariffRepository;

        public AbonentService(IRepository<Abonent> abonentRepository, IRepository<Tariff> tariffRepository)
        {
            _abonentRepository = abonentRepository;
            _tariffRepository = tariffRepository;
        }

        public async Task<IBaseResponce<Abonent>> Create(AbonentViewModel model)
        {
            try
            {
                var abonent = new Abonent();
                abonent.Contract_number = model.Contract_number;
                abonent.Name = model.Name;
                abonent.Phone = model.Phone;
                abonent.Birthday = model.Birthday;
                abonent.Adress = model.Adress;
                abonent.Passport = model.Passport;
                abonent.Tariff_ID = model.Tariff_ID;
                await _abonentRepository.Create(abonent);

                return new BaseResponse<Abonent>()
                {
                    Result = abonent,
                    Description = "Объект добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Abonent>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public IBaseResponce<List<Abonent>> Get()
        {
            try
            {
                var abonents = _abonentRepository.Get().ToList();
                if (!abonents.Any())
                {
                    return new BaseResponse<List<Abonent>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<Abonent>>()
                {
                    Result = abonents,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Abonent>>()
                {
                    Description = $"[GetAbonents] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<List<AbonentListViewModel>> GetAbonentView()
        {
            try
            {
                var abonents = _abonentRepository.Get().ToList();
                var abonentView = new List<AbonentListViewModel>();
                foreach (var item in abonents)
                {
                    AbonentListViewModel model = new AbonentListViewModel();

                    model.Abonent_ID = item.Abonent_ID;
                    model.Contract_number = item.Contract_number;
                    model.Name = item.Name;
                    model.Phone = item.Phone;
                    model.Birthday = item.Birthday.ToShortDateString();
                    model.Adress = item.Adress;
                    model.Passport = item.Passport;
                    var tariff = _tariffRepository.GetById(item.Tariff_ID).Result;
                    model.Tariff = tariff.Name;
                    abonentView.Add(model);
                }
                if (!abonentView.Any())
                {
                    return new BaseResponse<List<AbonentListViewModel>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<AbonentListViewModel>>()
                {
                    Result = abonentView,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<AbonentListViewModel>>()
                {
                    Description = $"[GetAbonents] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<Abonent> GetById(uint id)
        {
            try
            {
                var abonent = _abonentRepository.GetById(id).Result;
                if (abonent == null)
                {
                    return new BaseResponse<Abonent>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<Abonent>()
                {
                    Result = abonent,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Abonent>()
                {
                    Description = $"[GetAbonent] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<AbonentViewModel> GetByViewId(uint id)
        {
            try
            {
                var abonent = _abonentRepository.GetById(id).Result;
                if (abonent == null)
                {
                    return new BaseResponse<AbonentViewModel>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                AbonentViewModel abonentView = new AbonentViewModel();
                abonentView.Abonent_ID = abonent.Abonent_ID;
                abonentView.Adress = abonent.Adress;
                abonentView.Birthday = abonent.Birthday;
                abonentView.Tariff_ID = abonent.Tariff_ID;
                abonentView.Contract_number = abonent.Contract_number;
                abonentView.Name = abonent.Name;
                abonentView.Passport = abonent.Passport;
                abonentView.Phone = abonent.Phone;

                return new BaseResponse<AbonentViewModel>()
                {
                    Result = abonentView,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<AbonentViewModel>()
                {
                    Description = $"[GetAbonent] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponce<Abonent>> Remove(uint id)
        {
            try
            {
                var abonent = _abonentRepository.GetById(id).Result;
                if (abonent == null)
                {
                    return new BaseResponse<Abonent>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                await _abonentRepository.Delete(abonent);
                return new BaseResponse<Abonent>()
                {
                    Result = abonent,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Abonent>()
                {
                    Description = $"[GetAbonent] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponce<Abonent>> Update(AbonentViewModel model)
        {
            try
            {
                var abonent = new Abonent();
                abonent.Abonent_ID = model.Abonent_ID;
                abonent.Contract_number = model.Contract_number;
                abonent.Name = model.Name;
                abonent.Phone = model.Phone;
                abonent.Birthday = model.Birthday;
                abonent.Adress = model.Adress;
                abonent.Passport = model.Passport;
                abonent.Tariff_ID = model.Tariff_ID;
                await _abonentRepository.Update(abonent);

                return new BaseResponse<Abonent>()
                {
                    Result = abonent,
                    Description = "Объект обновлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Abonent>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
