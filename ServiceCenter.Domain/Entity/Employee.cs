using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceCenter.Domain.Entity
{
    public class Employee
    {
        public uint Employee_ID { get; set; }
        [Display(Name = "Должность")]
        public string Position { get; set; }
        [Display(Name = "ФИО")]
        public string Name { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Пользователь")]
        public uint? User_ID { get; set; }
        [ForeignKey("User_ID")]
        public virtual User User { get; set; }
    }
}
