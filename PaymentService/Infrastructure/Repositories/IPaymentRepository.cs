using PaymentService.Domain.Entities;

namespace PaymentService.Infrastructure.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> GetByIdAsync(Guid id);
        Task AddAsync(Payment payment);
        Task<List<Payment>> GetPaymentsByUserIdAsync(Guid userId);
    }

}
