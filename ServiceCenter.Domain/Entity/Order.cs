using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceCenter.Domain.Entity
{
    public class Order
    {
        public uint Order_ID { get; set; }
        public string Status { get; set; }
        public DateTime Order_date { get; set; }
        public DateTime? Order_close_date { get; set; }

        public uint Abonent_ID { get; set; }
        [ForeignKey("Abonent_ID")]
        public virtual Abonent Abonent { get; set; }

        public uint Employee_ID { get; set; }
        [ForeignKey("Employee_ID")]
        public virtual Employee Employee { get; set; }

        public virtual ICollection<OrderService> Services { get; set; }
    }
}
