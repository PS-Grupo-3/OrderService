using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PaymentStatusConfiguration : IEntityTypeConfiguration<PaymentStatus>
    {
        public void Configure(EntityTypeBuilder<PaymentStatus> builder) 
        {
            builder.HasKey(Ps => Ps.PaymentStatusId);

            builder.HasData
            (
                new PaymentStatus {PaymentStatusId=1,PaymentStatusName="Pending" },
                new PaymentStatus {PaymentStatusId=2,PaymentStatusName="Paid"},
                new PaymentStatus {PaymentStatusId = 3, PaymentStatusName = "Canceled" }
            );

        
        
        }



    }
}
