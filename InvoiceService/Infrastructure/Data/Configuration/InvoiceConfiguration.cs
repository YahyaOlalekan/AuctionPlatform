using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InvoiceService.Domain.Entities;

namespace InvoiceService.Infrastructure.Data.Configuration
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.AuctionRoomId)
                   .IsRequired();

            builder.Property(i => i.BidId)
                   .IsRequired();

            builder.Property(i => i.UserId)
                   .IsRequired();

            builder.Property(i => i.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(i => i.CreatedAt)
                   .IsRequired();

            builder.ToTable("Invoices");
        }
    }

}
