using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public class Discount
    {
        public int DiscountId { get; set; }
        public string Code { get; set; } = string.Empty;
        public decimal Percentage { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool IsActive { get; set; }
        public int MaxUses { get; set; }
        public int CurrentUses { get; set; }
    }
}
