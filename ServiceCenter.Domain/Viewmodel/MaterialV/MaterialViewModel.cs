using System.ComponentModel.DataAnnotations;

namespace ServiceCenter.Domain.Viewmodel.MaterialV
{
    public class MaterialViewModel
    {
        [Display(Name = "№")]
        public uint Material_ID { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Количество")]
        public int Count { get; set; }
        [Display(Name = "Цена")]
        public double Price { get; set; }
    }
}
