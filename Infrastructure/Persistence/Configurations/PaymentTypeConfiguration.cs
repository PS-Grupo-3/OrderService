using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.ToTable(nameof(PaymentType));

            builder.HasKey(p => p.PaymentId);

            builder.Property(p => p.PaymentName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData
            (
                new PaymentType { PaymentId = PaymentTypeIds.Cash, PaymentName = PaymentTypeNames.Cash },
                new PaymentType { PaymentId = PaymentTypeIds.MercadoPago, PaymentName = PaymentTypeNames.MercadoPago },
                new PaymentType { PaymentId = PaymentTypeIds.Visa, PaymentName = PaymentTypeNames.Visa },
                new PaymentType { PaymentId = PaymentTypeIds.MasterCard, PaymentName = PaymentTypeNames.MasterCard },
                new PaymentType { PaymentId = PaymentTypeIds.PayPal, PaymentName = PaymentTypeNames.PayPal }
            );
        }
    }
}
