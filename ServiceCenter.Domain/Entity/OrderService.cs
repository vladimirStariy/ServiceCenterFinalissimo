using System.ComponentModel.DataAnnotations;

namespace ServiceCenter.Domain.Entity
{
    public class OrderService
    {
        public uint OrderService_ID { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Цена")]
        public double Price { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
