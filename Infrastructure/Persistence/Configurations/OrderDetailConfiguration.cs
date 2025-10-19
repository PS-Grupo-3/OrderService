using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(d => d.DetailId);

            builder.HasIndex(d => d.OrderId);
            builder.HasIndex(d => d.TicketId);

            builder.Property(d => d.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.Property(d => d.Quantity)
                .IsRequired();

            builder.Property(d => d.Subtotal)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.HasOne(d => d.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
