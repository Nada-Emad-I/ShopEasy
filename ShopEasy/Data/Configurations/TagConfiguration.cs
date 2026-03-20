using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopEasy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Data.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {

            builder.ToTable("Tags", "shop");

            
            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            
            builder.HasIndex(t => t.Name)
                   .IsUnique()
                   .HasDatabaseName("IX_Tags_Name");
        }
    }
}
