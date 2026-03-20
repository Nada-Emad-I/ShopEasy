using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public class ProductTag
    {
        
        public int ProductId { get; set; }
        public int TagId { get; set; }

        
        public Product Product { get; set; } = null!;
        public Tag Tag { get; set; } = null!;
    }
}
