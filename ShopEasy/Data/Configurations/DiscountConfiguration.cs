using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopEasy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Data.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            
            builder.ToTable("Discounts", "shop");
            
            builder.Property(d => d.DiscountId)
                   .HasDefaultValueSql("NEXT VALUE FOR shop.DiscountSeq");

            
            builder.Property(d => d.Code)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.HasIndex(d => d.Code)
                   .IsUnique()
                   .HasDatabaseName("IX_Discounts_Code");

            
            builder.Property(d => d.Percentage)
                   .HasColumnType("decimal(5,2)");

            builder.Property(d => d.IsActive)
                   .HasDefaultValue(true);

            builder.Property(d => d.MaxUses)
                   .HasDefaultValue(100);
        }
    }

}
