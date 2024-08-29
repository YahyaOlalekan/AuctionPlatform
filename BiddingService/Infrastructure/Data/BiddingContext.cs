using BiddingService.Domain.Entities;
using BiddingService.Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BiddingService.Infrastructure.Data
{
    public class BiddingContext : DbContext
    {
        public BiddingContext(DbContextOptions<BiddingContext> options) : base(options)
        {
        }

        public DbSet<Bid> Bids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BidConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }

}
