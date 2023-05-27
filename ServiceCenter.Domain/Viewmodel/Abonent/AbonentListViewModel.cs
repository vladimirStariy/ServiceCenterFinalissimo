using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Viewmodel.Abonent
{
    public class AbonentListViewModel
    {
        [Display(Name = "№")]
        public uint Abonent_ID { get; set; }
        [Display(Name = "Номер договора")]
        public string Contract_number { get; set; }
        [Display(Name = "ФИО")]
        public string Name { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Display(Name = "Дата рождения")]
        public DateTime Birthday { get; set; }
        [Display(Name = "Адрес")]
        public string Adress { get; set; }
        [Display(Name = "Номер паспорта")]
        public string Passport { get; set; }
        [Display(Name = "Тариф")]
        public string Tariff { get; set; }
    }
}
