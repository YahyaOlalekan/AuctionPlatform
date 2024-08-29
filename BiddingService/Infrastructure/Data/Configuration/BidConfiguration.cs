using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BiddingService.Domain.Entities;

namespace BiddingService.Infrastructure.Data.Configuration
{
    public class BidConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.AuctionRoomId)
                   .IsRequired();

            builder.Property(b => b.UserId)
                   .IsRequired();

            builder.Property(b => b.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(b => b.Timestamp)
                   .IsRequired();

            builder.ToTable("Bids");
        }
    }

}
