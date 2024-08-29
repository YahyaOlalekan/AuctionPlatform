using RoomService.Infrastructure.Data;

namespace RoomService.Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuctionContext _context;

        public UnitOfWork(AuctionContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
