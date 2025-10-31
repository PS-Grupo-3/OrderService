namespace Application.Models.Responses
{
    public class UpdatedOrderResponse
    {
        public Guid Id { get; set; }
        public string Event { get; set; }
        public DateTime EventDate { get; set; }
        public string Venue { get; set; }
        public string Address { get; set; }
        public double TotalAmount { get; set; }
        public GenericResponse Payment { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Transaction { get; set; }
    }
}
