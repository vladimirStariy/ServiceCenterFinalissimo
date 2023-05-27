using System.ComponentModel.DataAnnotations;

namespace ServiceCenter.Domain.Viewmodel.User
{
    public class UserListViewModel
    {
        public uint User_ID { get; set; }
        [Display(Name = "Имя пользователя")]
        public string Login { get; set; }
        [Display(Name = "Роль")]
        public string Role { get; set; }
    }
}
