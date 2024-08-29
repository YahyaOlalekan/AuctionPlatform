using MediatR;

namespace PaymentService.Application.Commands
{
    public class ProcessPaymentCommand
    {
        public record Command(Guid InvoiceId, Guid UserId, decimal Amount) : IRequest<Guid>;

        //public class CommandHandler : IRequestHandler<Command, Guid>
        //{
        //    private readonly IPaymentRepository _paymentRepository;
        //    private readonly IUnitOfWork _unitOfWork;

        //    public CommandHandler(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
        //    {
        //        _paymentRepository = paymentRepository;
        //        _unitOfWork = unitOfWork;
        //    }

        //    public async Task<Guid> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        //    {
        //        // Logic to process the payment with an external payment gateway would go here
        //        var payment = new Payment(request.InvoiceId, request.UserId, request.Amount);

        //        // Simulate payment processing
        //        bool paymentSucceeded = true; // Assume the payment succeeds for now

        //        if (paymentSucceeded)
        //        {
        //            payment.MarkAsPaid();
        //        }
        //        else
        //        {
        //            payment.MarkAsFailed();
        //        }

        //        await _paymentRepository.AddAsync(payment);
        //        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //        return payment.Id;
        //    }
        //}

    }
}
