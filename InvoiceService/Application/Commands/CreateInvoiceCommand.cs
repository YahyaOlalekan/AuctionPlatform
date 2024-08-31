using InvoiceService.Application.UnitOfWork;
using InvoiceService.Domain.Entities;
using InvoiceService.Infrastructure.Repositories;
using MediatR;

namespace InvoiceService.Application.Commands
{
    public class CreateInvoiceCommand
    {
        public record CreateInvoice(Guid AuctionRoomId, Guid BidId, Guid UserId, decimal Amount) : IRequest<Guid>;

        public class CommandHandler : IRequestHandler<CreateInvoice, Guid>
        {
            private readonly IInvoiceRepository _invoiceRepository;
            private readonly IUnitOfWork _unitOfWork;

            public CommandHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
            {
                _invoiceRepository = invoiceRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Guid> Handle(CreateInvoice request, CancellationToken cancellationToken)
            {
                var invoice = new Invoice(request.AuctionRoomId, request.BidId, request.UserId, request.Amount);
                await _invoiceRepository.AddAsync(invoice);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return invoice.Id;
            }
        }

    }
}
