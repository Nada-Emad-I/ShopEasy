using Microsoft.EntityFrameworkCore;
using ShopEasy.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Helper
{
    public static class DiscountHelper
    {
        private static readonly AppDbContext context = new AppDbContext();
        public static void ApplyingDiscounCode(int OrderId, string Code)
        {
           
            var order = context.Orders.SingleOrDefault(o => o.OrderId == OrderId);
            var discount = context.Discounts.SingleOrDefault(c => c.Code == Code);
            if (!discount.IsActive) Console.WriteLine("Discount code Is Not Active");
            if (discount.ExpiresAt.HasValue) Console.WriteLine("Discount code Is Expired");
            if (discount.CurrentUses > discount.MaxUses) Console.WriteLine("You Reached to Maximum Uses of This Code");

            discount.CurrentUses += 1;
            var Sales = discount.Percentage / 100m;
            var TotalNow = order.TotalAmount - (order.TotalAmount * Sales);

            Console.WriteLine(discount.CurrentUses);
            Console.WriteLine(Sales);
            Console.WriteLine(TotalNow);

        }

        public static int DeleteAllExpiredDiscounts()
        {
            var DeletedDiscount = context.Discounts
                                         .Where(c => c.ExpiresAt < DateTime.UtcNow || !c.IsActive)
                                         .ExecuteDeleteAsync().GetAwaiter().GetResult();
            return DeletedDiscount;
        }

    }
}
