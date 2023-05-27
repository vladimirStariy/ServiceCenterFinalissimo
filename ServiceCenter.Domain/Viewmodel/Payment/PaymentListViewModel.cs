using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceCenter.Domain.Viewmodel.Payment
{
    public class PaymentListViewModel
    {
        [Display(Name = "#")]
        public uint Payment_ID { get; set; }
        [Display(Name = "Номер платежа")]
        public string Payment_number { get; set; }
        [Display(Name = "Дата платежа")]
        public DateTime Payment_date { get; set; }
        [Display(Name = "Сумма")]
        public double Price { get; set; }
        [Display(Name = "Абонент")]
        public string Abonent_name { get; set; }
    }
}
