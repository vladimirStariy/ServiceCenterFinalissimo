using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceCenter.Domain.Entity
{
    public class Payment
    {
        public uint Payment_ID { get; set; }
        public string Payment_number { get; set; }
        public DateTime Payment_date { get; set; }
        public double Price { get; set; }

        public uint Abonent_ID { get; set; }
        [ForeignKey("Abonent_ID")]
        public virtual Abonent Abonent { get; set; }
    }
}
