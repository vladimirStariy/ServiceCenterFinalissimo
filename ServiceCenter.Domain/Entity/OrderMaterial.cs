using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceCenter.Domain.Entity
{
    public class OrderMaterial
    {
        public uint OrderMaterial_ID { get; set; }

        public uint Material_ID { get; set; }
        [ForeignKey("Material_ID")]
        public virtual Material Material { get; set; }

        public uint Order_ID { get; set; }
        [ForeignKey("Order_ID")]
        public virtual Order Order { get; set; }

        public double Count { get; set; }
    }
}
