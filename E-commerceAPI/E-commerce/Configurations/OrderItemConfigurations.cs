using E_commerce.Models.JunctionTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_commerce.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Set table name (optional)
            builder.ToTable("OrderItems");

            // Set primary key
            builder.HasKey(oi => oi.OrderItemId);

            // Define foreign key relationships
            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure Quantity is required and greater than zero
            builder.Property(oi => oi.Quantity)
                .IsRequired();

            // Ensure UnitPrice is required
            builder.Property(oi => oi.UnitPrice)
                .IsRequired();
        }
    }
}
