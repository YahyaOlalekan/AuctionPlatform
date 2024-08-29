using InvoiceService.Infrastructure.Data;

namespace InvoiceService.Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InvoiceContext _context;

        public UnitOfWork(InvoiceContext context)
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
