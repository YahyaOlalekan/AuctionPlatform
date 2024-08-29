using InvoiceService.Domain.Entities;
using InvoiceService.Infrastructure.Repositories;
using MediatR;

namespace InvoiceService.Application.Queries
{
    public class GetInvoiceByIdQuery
    {
        public record Query(Guid Id) : IRequest<Invoice>;

        public class QueryHandler : IRequestHandler<Query, Invoice>
        {
            private readonly IInvoiceRepository _invoiceRepository;

            public QueryHandler(IInvoiceRepository invoiceRepository)
            {
                _invoiceRepository = invoiceRepository;
            }

            public async Task<Invoice> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _invoiceRepository.GetByIdAsync(request.Id);
            }
        }

    }
}
