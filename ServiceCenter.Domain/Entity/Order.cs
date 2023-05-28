using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceCenter.Domain.Entity
{
    public class Order
    {
        public uint Order_ID { get; set; }
        [Display(Name = "Статус")]
        public string Status { get; set; }
        [Display(Name = "Дата заявки")]
        public DateTime Order_date { get; set; }
        [Display(Name = "Дата закрытия")]
        public DateTime? Order_close_date { get; set; }

        [Display(Name = "Абонент")]
        public uint Abonent_ID { get; set; }
        [ForeignKey("Abonent_ID")]
        public virtual Abonent Abonent { get; set; }

        [Display(Name = "Сотрудник")]
        public uint Employee_ID { get; set; }
        [ForeignKey("Employee_ID")]
        public virtual Employee Employee { get; set; }

        public virtual ICollection<OrderService> Services { get; set; }
    }
}
