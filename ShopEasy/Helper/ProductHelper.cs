using Microsoft.EntityFrameworkCore;
using ShopEasy.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Helper
{
    public static class ProductHelper
    {
        private static readonly AppDbContext context = new AppDbContext();
        public static void GetAllAvaillableProducts()
        {
            

            var result = context.Products.Include(c => c.Category)
                                      .Select(c => new
                                      {
                                          Name = c.Name,
                                          Price = c.Price,
                                          CategoryName = c.Category.Name
                                      })
                                      .OrderBy(c => c.Price)
                                      .AsNoTracking();
            if (result.Any())
            {
                foreach (var item in result)
                {
                    Console.WriteLine($"Name: {item.Name} , Price: {item.Price} , Category: {item.CategoryName}");
                }
            }
            else
            {
                Console.WriteLine("No Products");
            }
        }
        public static void FilterProductsByNameOrCategory(string ProductName, string ProductCategory)
        {
            var product = context.Products.Where(p => p.Name.ToLower().Contains(ProductName) || p.Category.Name.ToLower().Contains(ProductCategory))
                            .Select(c => new
                            {
                                Name = $"Product {c.Name} is Found"
                            });
            if (product.Any())
            {
                foreach (var item in product)
                {
                    Console.WriteLine(item.Name);
                }
            }
            else
            {
                Console.WriteLine("Product Not Found");

            }

        }

        public static void CountAndAverageRating(int ProductId)
        {
            var ProductDetails = context.Products.Include(pt => pt.ProductTags)
                                                 .ThenInclude(t => t.Tag)
                                                 .Include(r => r.Reviews)
                                                 .SingleOrDefault(p => p.ProductId == ProductId);
            if (ProductDetails == null)
            {
                Console.WriteLine($"Product {ProductId} Is not found");
                return;
            }
            var AverageRating = context.Reviews
                .Where(r => r.ProductId == ProductId)
                .Select(r => (double?)r.Rating)
                .Average();


            var CountOfReviews = context.Reviews.Where(p => p.ProductId == ProductId).Count();

            Console.WriteLine(ProductDetails.Name);
            Console.WriteLine("Tags:");

            foreach (var product in ProductDetails.ProductTags)
            {
                Console.WriteLine(product.Tag.Name);
            }
            Console.WriteLine($"AverageRating: {AverageRating}, CountOfReveiws: {CountOfReviews}");

        }
        public static void GetThe5RatedProducts()
        {
            var Ratedproduct = context.Reviews.GroupBy(p => new { p.ProductId, p.Product.Name })
                                       .Select(p => new
                                       {
                                           ProductName = p.Key.Name,
                                           AverageRating = p.Average(r => r.Rating)
                                       }).OrderByDescending(p => p.AverageRating)
                                       .Take(5);
            foreach (var item in Ratedproduct)
            {
                Console.WriteLine($"PoductName: {item.ProductName}  ,  AverageRating: {item.AverageRating}");
            }
        }
    }
}
