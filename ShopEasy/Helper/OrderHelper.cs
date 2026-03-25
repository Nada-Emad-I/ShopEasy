using Microsoft.EntityFrameworkCore;
using ShopEasy.Data;
using ShopEasy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Helper
{
    public static class OrderHelper
    {
        private static readonly AppDbContext context = new AppDbContext();

        public static void GetallCurrentOrders(int Customerid)
        {
            var SeeOrder = context.Orders.Where(o => o.CustomerId == Customerid)
                                 .Include(o => o.OrderItems).Include(o => o.Payment)
                                 .OrderByDescending(o => o.PlacedAt);

            foreach (var order in SeeOrder)
            {
                Console.WriteLine($"OrderId: {order.OrderId} - Order Status: {order.Status} ");
                foreach (var ord in order.OrderItems)
                {
                    Console.WriteLine($"orderitems: {ord.OrderItemId} , quantity: {ord.Quantity}");
                }
            }
        }

        public static void CancelThePendingOrder(int orderId)
        {
            var Cancel = context.Orders.Where(o => o.Status == OrderStatus.Pending)
                           .Include(o => o.OrderItems)
                           .ThenInclude(p => p.Product)
                           .Include(p => p.Payment)
                           .SingleOrDefault(o => o.OrderId == orderId);
            Cancel.Status = OrderStatus.Cancelled;
            Cancel.Payment.Status = PaymentStatus.Refunded;
            foreach (var item in Cancel.OrderItems)
            {
                item.Product.StockQuantity += item.Quantity;
            }
            Console.WriteLine($"Order {orderId} is Cancelled");
            context.SaveChanges();

        }

        public static void SeeTotalRevenue()
        {
            var Revenue = context.Orders.Where(o => o.PlacedAt.HasValue && o.Status == OrderStatus.Delivered)
                                       .GroupBy(c => c.PlacedAt.Value.Month)
                                       .Select(c => new
                                       {
                                           Month = c.Key,
                                           TotalAmount = c.Sum(c => c.TotalAmount)
                                       });

            foreach (var order in Revenue)
            {
                Console.WriteLine($"Month: {order.Month}  ,  Amount: {order.TotalAmount}");
            }

        }

    }
}
