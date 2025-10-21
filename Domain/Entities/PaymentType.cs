using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PaymentType
    {

        public int PaymentId { get; set; }
        public string PaymentName { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
