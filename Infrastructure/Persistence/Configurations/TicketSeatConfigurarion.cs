using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class TicketSeatConfigurarion : IEntityTypeConfiguration<TicketSeat>
    {
        public void Configure(EntityTypeBuilder<TicketSeat> builder)
        {
            builder.ToTable("TicketSeat");

            builder.HasKey(t => t.TicketSeatId);

            builder.Property(t => t.TicketId)
                .IsRequired();

            builder.Property(t => t.EventId)
                .IsRequired();

            builder.Property(t => t.EventSectorId)
                .IsRequired();

            builder.Property(t => t.EventSeatId)
                .IsRequired();

            builder.Property(t => t.Price)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasOne(ts => ts.TicketRef)
                .WithMany(t => t.TicketSeats)
                .HasForeignKey(ts => ts.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
