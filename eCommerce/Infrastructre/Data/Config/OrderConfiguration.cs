using eCommerce.Core.entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Infrastructre.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(i => i.PaymentSummary, io => { io.WithOwner(); });
            builder.OwnsOne(o => o.ShipToAddress, sa =>
            {
                sa.WithOwner(); // This tells EF that ShipToAddress is owned by Order
            });


            builder.Property(s => s.Status)
                .HasConversion(
                    o => o.ToString(),
                    o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
                );

            builder.Property(s => s.Subtotal).HasColumnType("decimal(18,2)");
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

        }
    }
}
