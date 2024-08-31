using RoomService.Domain.Entities;

namespace RoomService.Infrastructure.Repositories
{
    public interface IAuctionRoomRepository
    {
        Task<AuctionRoom> GetByIdAsync(Guid id);
        Task AddAsync(AuctionRoom auctionRoom);
        Task UpdateAsync(AuctionRoom auctionRoom);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<AuctionRoom>> GetAllAsync();
    }

}
