using ServiceCenter.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ServiceCenter.Domain.Entity
{
    public class User
    {
        public uint User_ID { get; set; }
        [Display(Name = "Логин")]
        public string Username { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Display(Name = "Роль")]
        public Role Role { get; set; }
    }
}
