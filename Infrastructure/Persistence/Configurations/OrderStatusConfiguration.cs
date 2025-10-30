using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasKey(s => s.OrderStatusId);

            builder.Property(s => s.StatusName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData
            (
                new OrderStatus { OrderStatusId = 1, StatusName = "Pending" },
                new OrderStatus { OrderStatusId = 2, StatusName = "Paid" }
               
            );
        }
    }
}
