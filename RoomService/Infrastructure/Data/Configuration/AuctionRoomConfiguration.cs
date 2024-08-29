using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomService.Domain.Entities;

namespace RoomService.Infrastructure.Data.Configuration
{
    public class AuctionRoomConfiguration : IEntityTypeConfiguration<AuctionRoom>
    {
        public void Configure(EntityTypeBuilder<AuctionRoom> builder)
        {
            builder.HasKey(ar => ar.Id);

            builder.Property(ar => ar.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(ar => ar.StartTime)
                   .IsRequired();

            builder.Property(ar => ar.EndTime)
                   .IsRequired();

            builder.Property(ar => ar.IsActive)
                   .IsRequired();

            // If you need to map the _bids collection
            builder.Ignore(ar => ar.Bids);  // Assuming the bids are managed separately and not directly mapped to the database

            builder.ToTable("AuctionRooms");
        }
    }

}
