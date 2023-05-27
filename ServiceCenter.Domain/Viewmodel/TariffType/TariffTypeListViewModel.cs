using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Viewmodel.TariffType
{
    public class TariffTypeListViewModel
    {
        [Display(Name = "#")]
        public uint Id { get; set; }

        [Display(Name="Наименование")]
        public string Name { get; set; }
    }
}
