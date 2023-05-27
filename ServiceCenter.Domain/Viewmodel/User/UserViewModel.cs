using ServiceCenter.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ServiceCenter.Domain.Viewmodel.User
{
    public class UserViewModel
    {
        [Display(Name = "Логин")]
        public uint User_ID { get; set; }
        [Display(Name = "Логин")]
        public string Login { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Роль")]
        public int Role { get; set; }
    }
}
