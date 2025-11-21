using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable(nameof(OrderDetail));

            builder.HasKey(d => d.DetailId);

            builder.HasIndex(d => d.OrderId);

            builder.Property(d => d.IsSeat)
                .IsRequired();

            builder.Property(d => d.EventId)
                .IsRequired();

            builder.Property(d => d.EventSectorId)
                .IsRequired();

            builder.Property(d => d.EventSeatId)
                .IsRequired(false);

            builder.Property(d => d.Quantity)
               .IsRequired();

            builder.Property(d => d.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");           

            builder.Property(d => d.Subtotal)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.Property(d => d.DiscountAmount)
                .HasColumnType("decimal(18, 2)");

            builder.Property(d => d.TaxAmount)
                .HasColumnType("decimal(18, 2)");

            builder.Property(d => d.Total)
                .HasColumnType("decimal(18, 2)");

            builder.HasOne(d => d.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}