using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(nameof(Order));

            builder.HasKey(o => o.OrderId);

            builder.HasIndex(o => o.UserId);
            builder.HasIndex(o => o.PaymentId);
            builder.HasIndex(o => o.PaymentStatusId);

            builder.Property(o => o.EventName)
                .IsRequired();

            builder.Property(o => o.VenueName)
                .IsRequired();

            builder.Property(o => o.VenueAddress)
                .IsRequired();

            builder.Property(o => o.TotalAmount)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Property(o => o.Currency)
                .IsRequired()
                .HasMaxLength(3);

            builder.HasOne(o => o.PaymentType)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.PaymentId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne(o=>o.PaymentStatus)
                .WithMany(ps=>ps.Orders)
                .HasForeignKey(ps=>ps.PaymentStatusId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}