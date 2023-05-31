using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceCenter.Domain.Entity
{
    public class Payment
    {
        public uint Payment_ID { get; set; }
        [Display(Name = "Номер платежа")]
        public string Payment_number { get; set; }
        [Display(Name = "Дата выставления")]
        public DateTime Payment_vist { get; set; }
        [Display(Name = "Дата платежа")]
        public DateTime Payment_date { get; set; }
        [Display(Name = "Сумма")]
        public double Price { get; set; }
        [Display(Name = "Статус")]
        public string Status { get; set; }

        [Display(Name = "Абонент")]
        public uint Abonent_ID { get; set; }
        [ForeignKey("Abonent_ID")]
        public virtual Abonent Abonent { get; set; }
    }
}
