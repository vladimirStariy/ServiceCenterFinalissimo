using ServiceCenter.Data.Repository;
using ServiceCenter.Data.Repository.Implementation;
using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Enum;
using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Viewmodel.Abonent;
using ServiceCenter.Domain.Viewmodel.Employee;
using ServiceCenter.Service.Interfaces;

namespace ServiceCenter.Service.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IBaseResponce<Employee>> Create(EmployeeViewModel model)
        {
            try
            {
                var employee = new Employee();
                employee.Name = model.Name;
                employee.Phone = model.Phone;
                employee.Position = model.Position;
                employee.User_ID = model.User_ID;

                await _employeeRepository.Create(employee);

                return new BaseResponse<Employee>()
                {
                    Result = employee,
                    Description = "Объект добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Employee>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public IBaseResponce<List<Employee>> Get()
        {
            try
            {
                var employeers = _employeeRepository.Get().ToList();
                if (!employeers.Any())
                {
                    return new BaseResponse<List<Employee>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<Employee>>()
                {
                    Result = employeers,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Employee>>()
                {
                    Description = $"[GetEmployeers] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<Employee> GetById(uint id)
        {
            try
            {
                var employee = _employeeRepository.GetById(id).Result;
                if (employee == null)
                {
                    return new BaseResponse<Employee>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<Employee>()
                {
                    Result = employee,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Employee>()
                {
                    Description = $"[GetEmployee] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<Employee> GetByUserId(uint id)
        {
            try
            {
                var employee = _employeeRepository.Get().Where(x => x.User_ID == id).FirstOrDefault();
                if (employee == null)
                {
                    return new BaseResponse<Employee>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<Employee>()
                {
                    Result = employee,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Employee>()
                {
                    Description = $"[GetEmployee] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<List<EmployeeListViewModel>> GetEmployeeView()
        {
            try
            {
                var employers = _employeeRepository.Get().ToList();
                var employeeView = new List<EmployeeListViewModel>();
                foreach (var item in employers)
                {
                    EmployeeListViewModel model = new EmployeeListViewModel();
                    model.Employee_ID = item.Employee_ID;
                    model.Name = item.Name;
                    model.Phone = item.Phone;
                    model.Position = item.Position;
                    employeeView.Add(model);
                }
                if (!employeeView.Any())
                {
                    return new BaseResponse<List<EmployeeListViewModel>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<EmployeeListViewModel>>()
                {
                    Result = employeeView,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<EmployeeListViewModel>>()
                {
                    Description = $"[GetEmployee] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<EmployeeViewModel> GetViewById(uint id)
        {
            try
            {
                var employee = _employeeRepository.GetById(id).Result;
                
                if (employee == null)
                {
                    return new BaseResponse<EmployeeViewModel>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }

                EmployeeViewModel employeeViewModel = new EmployeeViewModel();
                employeeViewModel.Employee_ID = employee.Employee_ID;
                employeeViewModel.Name = employee.Name;
                employeeViewModel.Phone = employee.Phone;
                employeeViewModel.User_ID = employee.User_ID;
                employeeViewModel.Position = employee.Position;

                return new BaseResponse<EmployeeViewModel>()
                {
                    Result = employeeViewModel,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<EmployeeViewModel>()
                {
                    Description = $"[GetEmployee] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponce<Employee>> Remove(uint id)
        {
            try
            {
                var employee = _employeeRepository.GetById(id).Result;
                if (employee == null)
                {
                    return new BaseResponse<Employee>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                await _employeeRepository.Delete(employee);
                return new BaseResponse<Employee>()
                {
                    Result = employee,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Employee>()
                {
                    Description = $"[GetEmployee] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponce<Employee>> Update(EmployeeViewModel model)
        {
            try
            {
                var employee = new Employee();
                employee.Employee_ID = model.Employee_ID;
                employee.Name = model.Name;
                employee.Phone = model.Phone;
                employee.Position = model.Position;
                employee.User_ID = model.User_ID;

                await _employeeRepository.Update(employee);

                return new BaseResponse<Employee>()
                {
                    Result = employee,
                    Description = "Объект изменен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Employee>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
