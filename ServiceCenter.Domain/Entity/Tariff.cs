using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceCenter.Domain.Entity
{
    public class Tariff
    {
        public uint Tariff_ID { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Цена")]
        public double Price { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Вид услуги")]
        public uint TariffType_ID { get; set; }
        [ForeignKey("TariffType_ID")]
        public virtual TariffType TariffType { get; set; }
    }
}
