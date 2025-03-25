using E_commerce.Models.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_commerce.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.HasKey(o => o.OrderId);

            // Define the relationship for UserId
            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete for UserId

            // Define the relationship for ShippingDetailsId
            builder.HasOne(o => o.ShippingDetails)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.ShippingDetailsId)
                .OnDelete(DeleteBehavior.Cascade);  // Allow cascading delete for ShippingDetails
        }
    }
}


