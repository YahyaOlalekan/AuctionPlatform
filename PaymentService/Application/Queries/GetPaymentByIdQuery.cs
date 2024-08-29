using MediatR;
using PaymentService.Domain.Entities;
using PaymentService.Infrastructure.Repositories;

namespace PaymentService.Application.Queries
{
    public class GetPaymentByIdQuery
    {
        public record Query(Guid Id) : IRequest<Payment>;

        public class QueryHandler : IRequestHandler<Query, Payment>
        {
            private readonly IPaymentRepository _paymentRepository;

            public QueryHandler(IPaymentRepository paymentRepository)
            {
                _paymentRepository = paymentRepository;
            }

            public async Task<Payment> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _paymentRepository.GetByIdAsync(request.Id);
            }
        }

    }
}
