﻿namespace Domain.Entities
{
    public class PaymentStatus
    {
        public int PaymentStatusId { get; set; }
        public string PaymentStatusName { get; set;}

        public ICollection<Order>Orders { get; set;}
    }
}
