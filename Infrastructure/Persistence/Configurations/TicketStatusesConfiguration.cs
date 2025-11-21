using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class TicketStatusesConfiguration : IEntityTypeConfiguration<TicketStatus>
    {
        public void Configure(EntityTypeBuilder<TicketStatus> builder)
        {
            builder.ToTable("TicketStatus");

            builder.HasKey(s => s.StatusID);

            builder.Property(s => s.Name)
                .IsRequired()

                .HasColumnType("varchar(25)");
            builder.HasData(
                new TicketStatus { StatusID = 1, Name = "Available" },
                new TicketStatus { StatusID = 4, Name = "Expired" }
                );
        }
    }
}
