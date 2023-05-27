using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Response;
using ServiceCenter.Domain.Viewmodel.User;
using System.Security.Claims;

namespace ServiceCenter.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

        Task<IBaseResponce<User>> Create(UserViewModel model);
        Task<IBaseResponce<User>> Update(UserViewModel model);

        IBaseResponce<List<User>> Get();
        IBaseResponce<List<UserListViewModel>> GetUserView();
        IBaseResponce<User> GetById(uint id);
    }
}