using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopEasy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            
            builder.ToTable("Orders", "shop");

            
            builder.Property(o => o.Status)
                   .HasConversion<string>()
                   .HasMaxLength(30)
                   .HasDefaultValue(OrderStatus.Pending);

            
            builder.Property(o => o.TotalAmount)
                   .HasColumnType("decimal(18,2)");

            
            builder.Property(o => o.PlacedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            
            builder.HasIndex(o => o.Status)
                   .HasFilter("[Status] = 'Pending'")
                   .HasDatabaseName("IX_Orders_PendingStatus");

            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
