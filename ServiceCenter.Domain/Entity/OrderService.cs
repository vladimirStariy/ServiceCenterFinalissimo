namespace ServiceCenter.Domain.Entity
{
    public class OrderService
    {
        public uint OrderService_ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
