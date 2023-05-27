using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceCenter.Domain.Viewmodel.Order
{
    public class OrderListViewModel
    {
        [Display(Name = "№")]
        public uint Order_ID { get; set; }
        [Display(Name = "Статус")]
        public string Status { get; set; }
        [Display(Name = "Дата заказа")]
        public DateTime Order_date { get; set; }
        [Display(Name = "Дата исполнения")]
        public DateTime Order_close_date { get; set; }
        [Display(Name = "Абонент")]
        public string Abonent_name { get; set; }
        public uint Abonent_Id { get; set; }
        [Display(Name = "Сотрудник")]
        public string Employee_name { get; set; }
    }
}
