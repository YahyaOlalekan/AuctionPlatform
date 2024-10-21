using BiddingService.Application.UnitOfWork;
using BiddingService.Infrastructure.Repositories;
using MediatR;

namespace BiddingService.Application.Commands
{
    public class DeleteBidCommand
    {
        public record DeleteBid(Guid Id) : IRequest<Unit>;

        public class CommandHandler : IRequestHandler<DeleteBid, Unit>
        {
            private readonly IBidRepository _repository;
            private readonly IUnitOfWork _unitOfWork;

            public CommandHandler(IBidRepository repository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(DeleteBid request, CancellationToken cancellationToken)
            {
                await _repository.DeleteAsync(request.Id);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }

            //testing
        }

    }
}
