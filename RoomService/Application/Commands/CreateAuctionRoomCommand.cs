using FluentValidation;
using MediatR;
using RoomService.Application.UnitOfWork;
using RoomService.Domain.Entities;
using RoomService.Infrastructure.Repositories;

namespace RoomService.Application.Commands
{
    public class CreateAuctionRoomCommand
    {
        public record Command(string Name, DateTime StartTime, DateTime EndTime) : IRequest<Guid>;

        public class CommandHandler : IRequestHandler<Command, Guid>
        {
            private readonly IAuctionRoomRepository _repository;
            private readonly IUnitOfWork _unitOfWork;

            public CommandHandler(IAuctionRoomRepository repository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                var auctionRoom = new AuctionRoom(request.Name, request.StartTime, request.EndTime);
                await _repository.AddAsync(auctionRoom);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return auctionRoom.Id;
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
