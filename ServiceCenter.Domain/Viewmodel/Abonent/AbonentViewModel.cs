using System.ComponentModel.DataAnnotations;

namespace ServiceCenter.Domain.Viewmodel.Abonent
{
    public class AbonentViewModel
    {
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
        public uint Tariff_ID { get; set; }
    }
}