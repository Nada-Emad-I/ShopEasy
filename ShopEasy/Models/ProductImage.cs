using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public class ProductImage
    {
        public int ProductImageId { get; set; }
        public int ProductId { get; set; }
        public string Url { get; set; } = string.Empty;
        public string? AltText { get; set; }
        public bool IsPrimary { get; set; }

        
        public Product Product { get; set; } = null!;
    }
}
