using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ServiceCenter.Domain.Viewmodel.TariffType
{
    public class TariffTypeViewModel
    {
        [Display(Name = "#")]
        public uint Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }
    }
}
