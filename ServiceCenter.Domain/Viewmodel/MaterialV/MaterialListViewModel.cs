using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceCenter.Domain.Viewmodel.MaterialV
{
    public class MaterialListViewModel
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
