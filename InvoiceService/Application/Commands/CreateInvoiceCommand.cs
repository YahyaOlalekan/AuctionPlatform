using MediatR;

namespace InvoiceService.Application.Commands
{
    public class CreateInvoiceCommand
    {
        public record Command(Guid AuctionRoomId, Guid BidId, Guid UserId, decimal Amount) : IRequest<Guid>;

        //public class CommandHandler : IRequestHandler<Command, Guid>
        //{
        //    private readonly IInvoiceRepository _invoiceRepository;
        //    private readonly IUnitOfWork _unitOfWork;

        //    public CommandHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
        //    {
        //        _invoiceRepository = invoiceRepository;
        //        _unitOfWork = unitOfWork;
        //    }

        //    public async Task<Guid> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        //    {
        //        var invoice = new Invoice(request.AuctionRoomId, request.BidId, request.UserId, request.Amount);
        //        await _invoiceRepository.AddAsync(invoice);
        //        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //        return invoice.Id;
        //    }
        //}

    }
}
