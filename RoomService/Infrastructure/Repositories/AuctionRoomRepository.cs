using Microsoft.EntityFrameworkCore;
using RoomService.Domain.Entities;
using RoomService.Infrastructure.Data;

namespace RoomService.Infrastructure.Repositories
{
    public class AuctionRoomRepository : IAuctionRoomRepository
    {
        private readonly AuctionContext _context;

        public AuctionRoomRepository(AuctionContext context)
        {
            _context = context;
        }

        public async Task<AuctionRoom> GetByIdAsync(Guid id)
        {
            return await _context.AuctionRooms.FindAsync(id);
        }

        public async Task AddAsync(AuctionRoom auctionRoom)
        {
            await _context.AuctionRooms.AddAsync(auctionRoom);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AuctionRoom auctionRoom)
        {
            _context.AuctionRooms.Update(auctionRoom);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var auctionRoom = await GetByIdAsync(id);
            if (auctionRoom != null)
            {
                _context.AuctionRooms.Remove(auctionRoom);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AuctionRoom>> GetAllAsync()
        {
            return await _context.AuctionRooms.ToListAsync();
        }
    }
}
