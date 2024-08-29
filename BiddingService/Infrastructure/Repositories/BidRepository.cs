using BiddingService.Domain.Entities;
using BiddingService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BiddingService.Infrastructure.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly BiddingContext _context;

        public BidRepository(BiddingContext context)
        {
            _context = context;
        }

        public async Task<Bid> GetHighestBidAsync(Guid auctionRoomId)
        {
            return await _context.Bids
                                 .Where(b => b.AuctionRoomId == auctionRoomId)
                                 .OrderByDescending(b => b.Amount)
                                 .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Bid bid)
        {
            await _context.Bids.AddAsync(bid);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Bid>> GetBidsByAuctionRoomAsync(Guid auctionRoomId)
        {
            return await _context.Bids
                                 .Where(b => b.AuctionRoomId == auctionRoomId)
                                 .OrderByDescending(b => b.Timestamp)
                                 .ToListAsync();
        }
    }

}
