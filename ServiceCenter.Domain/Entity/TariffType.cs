using System.ComponentModel.DataAnnotations;

namespace ServiceCenter.Domain.Entity
{
    public class TariffType
    {
        public uint TariffType_ID { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
    }
}
