using ServiceCenter.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace ServiceCenter.Domain.Viewmodel.Order
{
    public class OrderFormViewModel
    {
        public uint id { get; set; }
        [Display(Name="Статус")]
        public string Status { get; set; }
        [Display(Name = "Дата заявки")]
        public DateTime Order_date { get; set; }
        [Display(Name = "Дата закрытия заявки")]
        public DateTime? Order_close_date { get; set; }

        [Display(Name = "Абонент")]
        public uint Abonent_ID { get; set; }

        [Display(Name = "Ответственный")]
        public uint Employee_ID { get; set; }

        [Display(Name = "Материалы")]
        public List<OrderMaterial> Materials { get; set; }

        [Display(Name = "Услуги")]
        public List<OrderService> Services { get; set; }
    }
}
