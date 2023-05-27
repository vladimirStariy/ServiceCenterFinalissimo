using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceCenter.Domain.Viewmodel.OrderServiceV
{
    public class OrderServiceListViewModel
    {
        [Display(Name = "OrderService_ID")]
        public uint OrderService_ID { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Цена")]
        public double Price { get; set; }
    }
}
