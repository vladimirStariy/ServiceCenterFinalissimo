using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceCenter.Domain.Viewmodel.Tariff
{
    public class TariffViewModel
    {
        public uint Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Стоимость")]
        public double Price { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Вид тарифного плана")]
        public uint TariffType_ID { get; set; }
    }
}
