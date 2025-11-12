using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder) 
        {
            builder.ToTable(nameof(OrderStatus));

            builder.HasKey(o => o.OrderStatusId);

            builder.HasData
            (
                new OrderStatus { OrderStatusId = OrderStatusIds.Pending, OrderStatusName = OrderStatusNames.Pending },
                new OrderStatus { OrderStatusId = OrderStatusIds.Paid, OrderStatusName = OrderStatusNames.Paid }
            );
        }
    }
}
