namespace Application.Models.Requests
{
    public class UpdateStatusRequest
    {
        public int Status { get; set; }
        public string? transactionId = $"MP-{new Guid()}";
    }
}
