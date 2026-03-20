using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }

    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public string Method { get; set; } = string.Empty;
        public PaymentStatus Status { get; set; }
        public DateTime? PaidAt { get; set; }
        public decimal Amount { get; set; }

        
        public Order Order { get; set; } = null!;
    }
}
