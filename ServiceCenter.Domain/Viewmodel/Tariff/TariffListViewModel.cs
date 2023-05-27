using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Viewmodel.Tariff
{
    public class TariffListViewModel
    {
        public int Id { get; set; }
        [Display (Name="Наименование")]
        public string Name { get; set; }

        [Display(Name = "Стоимость")]
        public double Price { get; set; }

        [Display(Name = "Тип тарифа")]
        public string TariffType { get; set; }
    }
}
