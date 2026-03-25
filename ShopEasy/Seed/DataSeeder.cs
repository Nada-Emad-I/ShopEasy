using ShopEasy.Data;
using ShopEasy.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopEasy.Seed
{
    public class DataSeeder
    {
        private readonly AppDbContext _context;
        public DataSeeder(AppDbContext context)
        {
            _context = context;
        }
        public async Task SeedAsync()
        {
            await SeedEntityAsync<Category>(_context.Categories, "JsonData/categories.json");
            await SeedEntityAsync<Tag>(_context.Tags, "JsonData/tags.json");
            await SeedEntityAsync<Customer>(_context.Customers, "JsonData/customers.json");
            await SeedEntityAsync<CustomerProfile>(_context.CustomerProfiles, "JsonData/customerProfiles.json");
            await SeedEntityAsync<Product>(_context.Products, "JsonData/products.json");
            await SeedEntityAsync<Order>(_context.Orders, "JsonData/orders.json");
            await SeedEntityAsync<OrderItem>(_context.OrderItems, "JsonData/orderItems.json");
            await SeedEntityAsync<Payment>(_context.Payments, "JsonData/payments.json");
            await SeedEntityAsync<Review>(_context.Reviews, "JsonData/reviews.json");

            // Product Tags 
            if (!_context.ProductTags.Any())
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "JsonData/productTags.json");

                if (!File.Exists(path))
                    throw new FileNotFoundException(path);
                var data = await File.ReadAllTextAsync(path);
                var entities = JsonSerializer.Deserialize<IEnumerable<ProductTag>>(data, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (entities is not null)
                {
                    var entityType = _context.Model.FindEntityType(typeof(ProductTag))!;
                    var tableName = entityType.GetTableName();
                    var schema = entityType.GetSchema() ?? "dbo";

                    var fullName = $"[{schema}].[{tableName}]";

                    using var transaction = await _context.Database.BeginTransactionAsync();
                    await _context.ProductTags.AddRangeAsync(entities);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
            }
            // Discounts
            if (!_context.Discounts.Any())
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "JsonData/discounts.json");

                if (!File.Exists(path))
                    throw new FileNotFoundException(path);
                var data = await File.ReadAllTextAsync(path);
                var entities = JsonSerializer.Deserialize<IEnumerable<Discount>>(data, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (entities is not null)
                {
                    var entityType = _context.Model.FindEntityType(typeof(Discount))!;
                    var tableName = entityType.GetTableName();
                    var schema = entityType.GetSchema() ?? "dbo";

                    var fullName = $"[{schema}].[{tableName}]";

                    using var transaction = await _context.Database.BeginTransactionAsync();
                    await _context.Discounts.AddRangeAsync(entities);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
            }
        }
        private async Task SeedEntityAsync<T>(DbSet<T> dbSet, string filePath) where T : class
        {
            if (dbSet.Any()) return;
            var path = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            if (!File.Exists(path))
                throw new FileNotFoundException(path);
            var data = await File.ReadAllTextAsync(path);

            var entities = JsonSerializer.Deserialize<IEnumerable<T>>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters =
            {
                new JsonStringEnumConverter()
            }
            });
            if (entities is not null)
            {
                var entityType = _context.Model.FindEntityType(typeof(T))!;
                var tableName = entityType.GetTableName();
                var schema = entityType.GetSchema() ?? "dbo";

                var fullName = $"[{schema}].[{tableName}]";

                using var transaction = await _context.Database.BeginTransactionAsync();

                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {fullName} ON");
                await dbSet.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {fullName} OFF");
                await transaction.CommitAsync();

            }
        }
    }
}
   
