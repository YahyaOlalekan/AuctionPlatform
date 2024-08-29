using BiddingService.Infrastructure.Data;

namespace BiddingService.Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BiddingContext _context;

        public UnitOfWork(BiddingContext context)
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
