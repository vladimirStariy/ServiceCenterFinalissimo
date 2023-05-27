using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Viewmodel.Abonent;
using ServiceCenter.Domain.Viewmodel.Order;

namespace ServiceCenter.Service.Interfaces
{
    public interface IAbonentService
    {
        Task<IBaseResponce<Abonent>> Create(AbonentViewModel model);
        Task<IBaseResponce<Abonent>> Update(AbonentViewModel model);
        Task<IBaseResponce<Abonent>> Remove(uint id);

        IBaseResponce<List<Abonent>> Get();
        IBaseResponce<List<AbonentListViewModel>> GetAbonentView();
        IBaseResponce<Abonent> GetById(uint id);
        IBaseResponce<AbonentViewModel> GetByViewId(uint id);
    }
}
