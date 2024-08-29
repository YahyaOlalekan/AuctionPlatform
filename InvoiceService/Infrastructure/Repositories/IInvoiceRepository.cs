using InvoiceService.Domain.Entities;

namespace InvoiceService.Infrastructure.Repositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice> GetByIdAsync(Guid id);
        Task AddAsync(Invoice invoice);
        Task<List<Invoice>> GetInvoicesByUserIdAsync(Guid userId);
    }

}
