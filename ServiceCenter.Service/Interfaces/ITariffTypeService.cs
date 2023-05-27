using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Viewmodel.TariffType;

namespace ServiceCenter.Service.Interfaces
{
    public interface ITariffTypeService
    {
        Task<IBaseResponce<TariffType>> Create(TariffTypeViewModel model);
        Task<IBaseResponce<TariffType>> Update(TariffTypeViewModel model);
        Task<IBaseResponce<TariffType>> Remove(uint id);

        IBaseResponce<List<TariffType>> Get();
        IBaseResponce<List<TariffTypeListViewModel>> GetTarifTypeView();
        IBaseResponce<TariffType> GetById(uint id);
    }
}
