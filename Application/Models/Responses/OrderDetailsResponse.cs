namespace Application.Models.Responses
{
    public class OrderDetailsResponse
    {
        public Guid DetailId { get; set; }
        public Guid TicketId { get; set; }
        public string Sector {  get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double SubTotal { get; set; }
        public double? Discount { get; set; }
        public double? Tax { get; set; }
    }
}
