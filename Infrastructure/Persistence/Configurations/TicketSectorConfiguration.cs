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
    public class TicketSectorConfiguration : IEntityTypeConfiguration<TicketSector>
    {
        public void Configure(EntityTypeBuilder<TicketSector> builder)
        {
            builder.ToTable("TicketSector");

            builder.HasKey(t => t.TicketSectorId);

            builder.Property(t => t.TicketId)
                .IsRequired();

            builder.Property(t => t.EventId)
                .IsRequired();

            builder.Property(t => t.EventSectorId)
                .IsRequired();

            builder.Property(t => t.Quantity)
                .IsRequired();

            builder.Property(t => t.UnitPrice)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasOne(ts => ts.TicketRef)
                .WithMany(t => t.TicketSectors)
                .HasForeignKey(ts => ts.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
