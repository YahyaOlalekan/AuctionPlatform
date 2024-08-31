using BiddingService.Domain.Entities;

namespace BiddingService.Infrastructure.Repositories
{
    public interface IBidRepository
    {
        Task<Bid> GetHighestBidAsync(Guid auctionRoomId);
        Task AddAsync(Bid bid);
        Task<List<Bid>> GetBidsByAuctionRoomAsync(Guid auctionRoomId);
        Task<Bid> GetByIdAsync(Guid id);
        Task<IEnumerable<Bid>> GetAllAsync();  
        Task UpdateAsync(Bid bid);  
        Task DeleteAsync(Guid id);
    }

}
