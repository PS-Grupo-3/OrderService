namespace Domain.Entities
{
    public class PaymentType
    {

        public int PaymentId { get; set; }
        public string PaymentName { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
