using Microsoft.EntityFrameworkCore;
using ShopEasy.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Helper
{
    public static class Help
    {
        private static readonly AppDbContext context = new AppDbContext();
        public static void Lazy_LoadProductReviews()
        {
            var product = context.Products.FirstOrDefault();
            Console.WriteLine("Product is Loaded");

            foreach (var item in product.Reviews)
            {
                Console.WriteLine(item.Comment);
            }
        }

        public static void LoadCustomerDataUsingSplitQueries(int customerid)
        {
            var customer = context.Customers.Include(c => c.Orders)
                                           .ThenInclude(o => o.OrderItems)
                                           .Include(c => c.Reviews)
                                           .AsSplitQuery()
                                           .SingleOrDefault(c => c.CustomerId == customerid);
            if (customer == null)
            {
                Console.WriteLine("Customer not found");
                return;
            }
            Console.WriteLine($"Customer Name: {customer.FullName}");
            Console.WriteLine($"Orders:");

            foreach (var item in customer.Orders)
            {
                Console.WriteLine($"Id: {item.OrderId} - PlacedAt {item.PlacedAt} - {item.OrderItems.Count()}");
                foreach (var item1 in item.OrderItems)
                {
                    Console.WriteLine($"  Item: {item1.OrderItemId} - Quantity: {item1.Quantity}");
                }
            }
            Console.WriteLine($"Reviews: {customer.Reviews.Count}");
            foreach (var item in customer.Reviews)
            {
                Console.WriteLine($"{item.CustomerId}");
                Console.WriteLine($"Rating:{item.Rating} - Comment: {item.Comment}");
            }
        }
        public static void CustomersWithNoOrders()
        {
            var Customers_WithNoOrders = context.Customers
                                         .GroupJoin(
                                               context.Orders,
                                               C => C.CustomerId,
                                               O => O.CustomerId,
                                               (c, o) => new { c, o }
                                         ).Where(x => !x.o.Any())
                                         .Select(x => new
                                         {
                                             FullName = x.c.FullName,
                                             Email = x.c.Email
                                         });

            foreach (var item in Customers_WithNoOrders)
            {
                Console.WriteLine($"{item.FullName}  -  {item.Email}");
            }
        }

        public static void ProductsRankedByTotalQuantitySold()
        {
            var ProducAndOrderItems = context.Products
                                           .Join(context.OrderItems,
                                            product => product.ProductId,
                                            Oitem => Oitem.ProductId,
                                            (p, o) => new
                                            {
                                                Pid = p.ProductId,
                                                pName = p.Name,
                                                orderQ = o.Quantity
                                            }
                                           ).GroupBy(c => new { c.Pid, c.pName })
                                           .Select(c => new
                                           {
                                               ProductName = c.Key.pName,
                                               TotalSold = c.Sum(x => x.orderQ)
                                           }).OrderByDescending(o => o.TotalSold);
            foreach (var item in ProducAndOrderItems)
            {
                Console.WriteLine($"Product: {item.ProductName} - Total_Sold: {item.TotalSold}");
            }
        }
    }
}
