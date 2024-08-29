using Microsoft.EntityFrameworkCore;
using RoomService.Domain.Entities;
using RoomService.Infrastructure.Data.Configuration;

namespace RoomService.Infrastructure.Data
{
    public class AuctionContext : DbContext
    {
        public AuctionContext(DbContextOptions<AuctionContext> options) : base(options)
        {
        }

        public DbSet<AuctionRoom> AuctionRooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuctionRoomConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }

}
