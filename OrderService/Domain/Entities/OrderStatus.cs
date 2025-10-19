namespace Domain.Entities
{
    public class OrderStatus
    {
        public int OrderStatusId { get; set; }
        public string StatusName { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
