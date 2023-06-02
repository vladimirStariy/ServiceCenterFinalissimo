using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Viewmodel.Abonent;
using ServiceCenter.Domain.Viewmodel.Employee;

namespace ServiceCenter.Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<IBaseResponce<Employee>> Create(EmployeeViewModel model);
        Task<IBaseResponce<Employee>> Update(EmployeeViewModel model);
        Task<IBaseResponce<Employee>> Remove(uint id);

        IBaseResponce<Employee> GetByUserId(uint id);
        IBaseResponce<List<Employee>> Get();
        IBaseResponce<List<EmployeeListViewModel>> GetEmployeeView();
        IBaseResponce<Employee> GetById(uint id);
        IBaseResponce<EmployeeViewModel> GetViewById(uint id);
    }
}
