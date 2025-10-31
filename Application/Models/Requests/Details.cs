namespace Application.Models.Requests
{
    public class Details
    {
        public Guid TicketId { get; set; }
        public string Sector {  get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double? Discount { get; set; }
        public double? Tax { get; set; }
    }
}
