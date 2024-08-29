using MediatR;
using RoomService.Application.UnitOfWork;
using RoomService.Infrastructure.Repositories;

namespace RoomService.Application.Commands
{
    public class DeleteAuctionRoomCommand
    {
        public record Command(Guid Id) : IRequest<Unit>;

        public class CommandHandler : IRequestHandler<Command, Unit>
        {
            private readonly IAuctionRoomRepository _repository;
            private readonly IUnitOfWork _unitOfWork;

            public CommandHandler(IAuctionRoomRepository repository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _repository.DeleteAsync(request.Id);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

    }
}
