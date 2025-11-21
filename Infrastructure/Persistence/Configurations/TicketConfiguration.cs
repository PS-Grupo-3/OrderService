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
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket");
            builder.HasKey(t => t.TicketId);

            builder.Property(t => t.OrderId)
                .IsRequired();

            builder.Property(t => t.UserId)
                .IsRequired();

            builder.Property(t => t.EventId)
                .IsRequired();

            builder.Property(t => t.StatusId)
                .IsRequired();

            builder.Property(t => t.Created)
                .IsRequired();

            builder.Property(t => t.Updated);

            // uno a muchos - TicketStatus  Ticket
            builder.HasOne(ticket => ticket.StatusRef)
                .WithMany(TStatus => TStatus.Tickets)
                .HasForeignKey(t => t.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
