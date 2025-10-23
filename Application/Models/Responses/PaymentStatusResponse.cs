
using MediatR.NotificationPublishers;

namespace Application.Models.Responses
{
    public class PaymentStatusResponse
    {
        public int statusId { get; set; }
        public string StatusName { get; set; }
    }
}
