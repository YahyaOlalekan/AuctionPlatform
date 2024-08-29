using FluentValidation;
using MediatR;
using RoomService.Application.UnitOfWork;
using RoomService.Infrastructure.Repositories;

namespace RoomService.Application.Commands
{
    public class UpdateAuctionRoomCommand
    {
        public record Command(Guid Id, string Name, DateTime StartTime, DateTime EndTime) : IRequest<Unit>;

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
                var auctionRoom = await _repository.GetByIdAsync(request.Id);
                if (auctionRoom == null)
                    throw new KeyNotFoundException("Auction room not found.");

                auctionRoom.UpdateDetails(request.Name, request.StartTime, request.EndTime);
                await _repository.UpdateAsync(auctionRoom);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Name is required.")
                    .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

                RuleFor(x => x.StartTime)
                    .GreaterThan(DateTime.UtcNow).WithMessage("Start time must be in the future.");

                RuleFor(x => x.EndTime)
                    .GreaterThan(x => x.StartTime).WithMessage("End time must be after the start time.");
            }
        }

    }
}
