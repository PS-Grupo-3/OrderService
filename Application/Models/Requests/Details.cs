namespace Application.Models.Requests
{
    public class Details
    {
        public Guid SectorId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Tax { get; set; }
    }
}
