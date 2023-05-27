using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceCenter.Domain.Viewmodel.Employee
{
    public class EmployeeListViewModel
    {
        [Display(Name = "№")]
        public uint Employee_ID { get; set; }
        [Display(Name = "ФИО")]
        public string Name { get; set; }
        [Display(Name = "Должность")]
        public string Position { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
    }
}
