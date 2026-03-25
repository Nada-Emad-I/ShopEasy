using Microsoft.EntityFrameworkCore;
using ShopEasy.Data;
using ShopEasy.Helper;
using ShopEasy.Models;
using ShopEasy.Seed;

namespace ShopEasy
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new AppDbContext();

            var seeder = new DataSeeder(context);
            seeder.SeedAsync();



            //CustomerHelper.AddCustomerAndProfile();
            //string Input = Console.ReadLine();
            //int CustomerId = int.Parse(Input);
            //var customer = CustomerHelper.GetCustomerDetails(CustomerId);

            //Console.WriteLine($"Customer Name: {customer.FullName} | Address: {customer.Profile?.Address}");

            //foreach (var order in customer.Orders)
            //{
            //    Console.WriteLine($"Order ID: {order.OrderId}, Status: {order.Status}, Total: {order.TotalAmount}");
            //}

            //string InputId = Console.ReadLine();
            //string InputAddress = Console.ReadLine();
            //int CustId = int.Parse(InputId);
            //CustomerHelper.UpdateAdressProfile(CustId, InputAddress);

            //ProductHelper.GetAllAvaillableProducts();


            //string ProductName = Console.ReadLine();
            //string ProductCategory = Console.ReadLine();
            //ProductHelper.FilterProductsByNameOrCategory(ProductName, ProductCategory);
            //string Input = Console.ReadLine();
            //int productID = int.Parse(Input);
            //ProductHelper.CountAndAverageRating(productID);


            //ProductHelper.GetThe5RatedProducts();


            //var CreateNewOrder = context.Orders.Include(o => o.OrderItems)
            //                                        .ThenInclude(p => p.Product)
            //                                   .Include(p => p.Payment);

            //string Input = Console.ReadLine();
            //int CustomerId = int.Parse(Input);

            //OrderHelper.GetallCurrentOrders(CustomerId);

            //string Input = Console.ReadLine();
            //int orderId = int.Parse(Input);
            //OrderHelper.CancelThePendingOrder(orderId);
            //context.SaveChanges();


            //OrderHelper.SeeTotalRevenue();

            //var PendingOrders = context.Orders.FromSqlRaw("Exec GetPendingOrders").ToList();

            //string Code = Console.ReadLine();
            //string Input = Console.ReadLine();
            //int orderId = int.Parse(Input);
            //DiscountHelper.ApplyingDiscounCode(orderId, Code);
            //context.SaveChanges();



            //Console.WriteLine(DiscountHelper.DeleteAllExpiredDiscounts());



            Help.Lazy_LoadProductReviews();

            //OutPut not Contain Review
            string Input = Console.ReadLine();
            int CustomerId = int.Parse(Input);
            Help.LoadCustomerDataUsingSplitQueries(CustomerId);

            Help.CustomersWithNoOrders();

            Help.ProductsRankedByTotalQuantitySold();

        }
       
        
    }

}
