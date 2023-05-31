using ServiceCenter.Data.Repository;
using ServiceCenter.Data.Repository.Implementation;
using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Enum;
using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Viewmodel.MaterialV;
using ServiceCenter.Domain.Viewmodel.Payment;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.Service.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment> _paymentRepository;

        public PaymentService(IRepository<Payment> paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IBaseResponce<Payment>> Create(PaymentViewModel model)
        {
            try
            {
                var payment = new Payment();
                payment.Payment_date = model.Payment_date;
                payment.Payment_vist = model.Payment_vist;
                payment.Payment_number = model.Payment_number;
                payment.Price = model.Price;
                payment.Status = model.Status;
                payment.Abonent_ID = model.Abonent_ID;

                await _paymentRepository.Create(payment);

                return new BaseResponse<Payment>()
                {
                    Result = payment,
                    Description = "Объект добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Payment>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public IBaseResponce<List<Payment>> Get()
        {
            try
            {
                var payments = _paymentRepository.Get().ToList();
                if (!payments.Any())
                {
                    return new BaseResponse<List<Payment>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<Payment>>()
                {
                    Result = payments,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Payment>>()
                {
                    Description = $"[GetPayments] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<Payment> GetById(uint id)
        {
            try
            {
                var payment = _paymentRepository.GetById(id).Result;
                if (payment == null)
                {
                    return new BaseResponse<Payment>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<Payment>()
                {
                    Result = payment,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Payment>()
                {
                    Description = $"[GetPayment] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<List<PaymentListViewModel>> GetPaymentView()
        {
            try
            {
                var payments = _paymentRepository.Get().ToList();
                var paymentsView = new List<PaymentListViewModel>();
                foreach (var item in payments)
                {
                    PaymentListViewModel model = new PaymentListViewModel();
                    model.Payment_ID = item.Payment_ID;
                    model.Payment_number = item.Payment_number;
                    model.Payment_date = item.Payment_date;
                    model.Payment_vist = item.Payment_vist;
                    model.Status = item.Status;
                    model.Price = item.Price;
                    model.Abonent_name = item.Abonent.Name;
                    paymentsView.Add(model);
                }
                if (!paymentsView.Any())
                {
                    return new BaseResponse<List<PaymentListViewModel>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<PaymentListViewModel>>()
                {
                    Result = paymentsView,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<PaymentListViewModel>>()
                {
                    Description = $"[GetPayments] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponce<Payment>> Remove(uint id)
        {
            try
            {
                var model = _paymentRepository.GetById(id).Result;
                if (model == null)
                {
                    return new BaseResponse<Payment>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                await _paymentRepository.Delete(model);
                return new BaseResponse<Payment>()
                {
                    Result = model,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Payment>()
                {
                    Description = $"[GetPayment] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public Task<IBaseResponce<Payment>> Update(PaymentViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
