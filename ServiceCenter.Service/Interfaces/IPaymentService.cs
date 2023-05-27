using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Viewmodel.Order;
using ServiceCenter.Domain.Viewmodel.Payment;

namespace ServiceCenter.Service.Interfaces
{
    public interface IPaymentService
    {
        Task<IBaseResponce<Payment>> Create(PaymentViewModel model);
        Task<IBaseResponce<Payment>> Update(PaymentViewModel model);
        Task<IBaseResponce<Payment>> Remove(uint id);

        IBaseResponce<List<Payment>> Get();
        IBaseResponce<List<PaymentListViewModel>> GetPaymentView();
        IBaseResponce<Payment> GetById(uint id);
    }
}
