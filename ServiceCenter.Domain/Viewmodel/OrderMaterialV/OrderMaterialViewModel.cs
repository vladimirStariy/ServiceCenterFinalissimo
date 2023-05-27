using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Viewmodel.OrderMaterialV
{
    public class OrderMaterialViewModel
    {
        public uint id { get; set; }
        [Display(Name="Материал")]
        public uint Material_ID { get; set; }
        [Display(Name = "Заявка")]
        public uint Order_ID { get; set; }
        [Display(Name = "Количество")]
        public double Count { get; set; }
    }
}
