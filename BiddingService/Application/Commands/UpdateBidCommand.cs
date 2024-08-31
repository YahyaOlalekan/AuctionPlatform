using BiddingService.Application.UnitOfWork;
using BiddingService.Infrastructure.Repositories;
using MediatR;

namespace BiddingService.Application.Commands
{
    public class UpdateBidCommand
    {
        public record UpdateBid(Guid Id, decimal Amount) : IRequest<Unit>;

        public class CommandHandler : IRequestHandler<UpdateBid, Unit>
        {
            private readonly IBidRepository _repository;
            private readonly IUnitOfWork _unitOfWork;

            public CommandHandler(IBidRepository repository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(UpdateBid request, CancellationToken cancellationToken)
            {
                var bid = await _repository.GetByIdAsync(request.Id);
                if (bid == null)
                    throw new KeyNotFoundException("Bid not found.");

                bid.UpdateAmount(request.Amount);  
                await _repository.UpdateAsync(bid);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

    }
}
