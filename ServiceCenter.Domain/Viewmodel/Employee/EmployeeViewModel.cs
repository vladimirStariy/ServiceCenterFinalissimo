using System.ComponentModel.DataAnnotations;

namespace ServiceCenter.Domain.Viewmodel.Employee
{
    public class EmployeeViewModel
    {
        public uint Employee_ID { get; set; }
        [Display(Name = "ФИО")]
        public string Name { get; set; }
        [Display(Name = "Должность")]
        public string Position { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        public uint? User_ID { get; set; }
    }
}
