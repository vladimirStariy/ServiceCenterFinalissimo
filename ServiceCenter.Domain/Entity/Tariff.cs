using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceCenter.Domain.Entity
{
    public class Tariff
    {
        public uint Tariff_ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public uint TariffType_ID { get; set; }
        [ForeignKey("TariffType_ID")]
        public virtual TariffType TariffType { get; set; }
    }
}
