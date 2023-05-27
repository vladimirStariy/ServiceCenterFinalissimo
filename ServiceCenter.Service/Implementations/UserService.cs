using Azure;
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Data.Repository;
using ServiceCenter.Data.Repository.Implementation;
using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Enum;
using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Viewmodel.User;
using ServiceCenter.Service.Interfaces;
using System.Net.Http;
using System.Security.Claims;

namespace ServiceCenter.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "Cookies");
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userRepository.Get().FirstOrDefaultAsync(x => x.Username == model.Username);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден"
                    };
                }

                if (user.Password != model.Password)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль или логин"
                    };
                }
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Result = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<List<User>> Get()
        {
            try
            {
                var users = _userRepository.Get().ToList();
                if (!users.Any())
                {
                    return new BaseResponse<List<User>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<User>>()
                {
                    Result = users,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<User>>()
                {
                    Description = $"[GetUsers] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<List<UserListViewModel>> GetUserView()
        {
            try
            {
                var users = _userRepository.Get().ToList();
                var userView = new List<UserListViewModel>();
                foreach (var item in users)
                {
                    UserListViewModel model = new UserListViewModel();
                    model.User_ID = item.User_ID;
                    model.Login = item.Username;
                    model.Role = item.Role.ToString();
                    userView.Add(model);
                }
                if (!userView.Any())
                {
                    return new BaseResponse<List<UserListViewModel>>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<UserListViewModel>>()
                {
                    Result = userView,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<UserListViewModel>>()
                {
                    Description = $"[GetUsers] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponce<User> GetById(uint id)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponce<User>> Create(UserViewModel model)
        {
            try
            {
                var user = new User()
                {
                    Username = model.Login,
                    Password = model.Password,
                    Role = (Role)Convert.ToInt32(model.Role)
                };

                await _userRepository.Create(user);

                return new BaseResponse<User>()
                {
                    Result = user,
                    Description = "Пользователь добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<User>> Update(UserViewModel model)
        {
            try
            {
                var user = new User()
                {
                    User_ID = model.User_ID,
                    Username = model.Login,
                    Password = model.Password,
                    Role = (Role)Convert.ToInt32(model.Role)
                };

                await _userRepository.Update(user);

                return new BaseResponse<User>()
                {
                    Result = user,
                    Description = "Пользователь изменен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
