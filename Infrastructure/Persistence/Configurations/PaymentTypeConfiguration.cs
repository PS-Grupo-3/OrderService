using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.HasKey(p => p.PaymentId);
            builder.Property(p => p.PaymentName).IsRequired().HasMaxLength(100);



            builder.HasData
            (
                new PaymentType { PaymentId = 1, PaymentName = "Efectivo" },
                new PaymentType { PaymentId = 2, PaymentName = "Mercado Pago" },
                new PaymentType { PaymentId = 3, PaymentName = "Metodo bancario" }
            );
        }
    }
}
