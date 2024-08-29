using InvoiceService.Domain.Entities;
using InvoiceService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceService.Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceContext _context;

        public InvoiceRepository(InvoiceContext context)
        {
            _context = context;
        }

        public async Task<Invoice> GetByIdAsync(Guid id)
        {
            return await _context.Invoices.FindAsync(id);
        }

        public async Task AddAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Invoice>> GetInvoicesByUserIdAsync(Guid userId)
        {
            return await _context.Invoices
                                 .Where(i => i.UserId == userId)
                                 .OrderByDescending(i => i.CreatedAt)
                                 .ToListAsync();
        }
    }

}
