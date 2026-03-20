using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopEasy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.ToTable("Customers", "shop");

            builder.HasKey(c => c.CustomerId);
            builder.Property(c => c.CustomerId)
                   .HasColumnName("customer_id");

            builder.Property(c => c.FullName)
                   .IsRequired()
                   .HasMaxLength(150)
                   .HasColumnName("full_name")
                   .HasComment("Customer full legal name");


            builder.Property(c => c.Email)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.HasIndex(c => c.Email)
                   .IsUnique()
                   .HasDatabaseName("IX_Customers_Email");


            builder.Property(c => c.PhoneNumber)
                   .HasMaxLength(20);

            builder.Property(c => c.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
