using PaymentService.Infrastructure.Data;

namespace PaymentService.Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PaymentContext _context;

        public UnitOfWork(PaymentContext context)
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
