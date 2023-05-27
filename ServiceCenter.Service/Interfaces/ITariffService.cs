using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Viewmodel.Tariff;

namespace ServiceCenter.Service.Interfaces
{
    public interface ITariffService
    {
        Task<IBaseResponce<Tariff>> Create(TariffViewModel model);
        Task<IBaseResponce<Tariff>> Update(TariffViewModel model);
        Task<IBaseResponce<Tariff>> Remove(uint id);

        IBaseResponce<List<Tariff>> Get();
        IBaseResponce<List<TariffListViewModel>> GetTariffView();
        IBaseResponce<Tariff> GetById(uint id);
    }
}
