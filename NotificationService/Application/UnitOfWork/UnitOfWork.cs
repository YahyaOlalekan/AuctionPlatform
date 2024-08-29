using NotificationService.Infrastructure.Data;

namespace NotificationService.Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NotificationContext _context;

        public UnitOfWork(NotificationContext context)
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
