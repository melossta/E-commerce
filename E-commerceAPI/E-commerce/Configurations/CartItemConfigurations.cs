using E_commerce.Models.JunctionTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_commerce.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            // Set table name (optional)
            builder.ToTable("CartItems");

            // Set primary key
            builder.HasKey(ci => ci.CartItemId);

            // Define foreign key relationships
            builder.HasOne(ci => ci.ShoppingCart)
                .WithMany(sc => sc.CartItems)
                .HasForeignKey(ci => ci.ShoppingCartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure Quantity is required and greater than zero
            builder.Property(ci => ci.Quantity)
                .IsRequired();
        }
    }
}
