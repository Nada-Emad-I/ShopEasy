using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopEasy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            
            builder.ToTable("Payments", "shop");

            builder.Property(p => p.Method)
                   .IsRequired()
                   .HasMaxLength(50);

            
            builder.Property(p => p.Status)
                   .HasConversion<string>()
                   .HasMaxLength(30);

            
            builder.Property(p => p.Amount)
                   .HasColumnType("decimal(18,2)");

            
            builder.HasOne(p => p.Order)
                   .WithOne(o => o.Payment)
                   .HasForeignKey<Payment>(p => p.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
