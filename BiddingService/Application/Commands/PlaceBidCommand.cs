using FluentValidation;
using MediatR;
using BiddingService.Application.UnitOfWork;
using BiddingService.Infrastructure.Repositories;
using BiddingService.Application.Services.Events;
using BiddingService.Domain.Entities;
using BiddingService.Application.Services;

namespace BiddingService.Application.Commands
{
    public class PlaceBidCommand
    {
        public record Command(Guid AuctionRoomId, Guid UserId, decimal Amount) : IRequest<Guid>;

        public class CommandHandler : IRequestHandler<Command, Guid>
        {
            private readonly IBidRepository _bidRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly BidEventPublisher _bidEventPublisher;

            public CommandHandler(IBidRepository bidRepository, IUnitOfWork unitOfWork, BidEventPublisher bidEventPublisher)
            {
                _bidRepository = bidRepository;
                _unitOfWork = unitOfWork;
                _bidEventPublisher = bidEventPublisher;
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                var highestBid = await _bidRepository.GetHighestBidAsync(request.AuctionRoomId);

                if (highestBid != null && request.Amount <= highestBid.Amount)
                    throw new InvalidOperationException("Bid amount must be higher than the current highest bid.");

                var bid = new Bid(request.AuctionRoomId, request.UserId, request.Amount);
                await _bidRepository.AddAsync(bid);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // Publish the new highest bid event
                var newHighestBidEvent = new NewHighestBidEvent(
                    auctionRoomId: bid.AuctionRoomId,
                    bidId: bid.Id,
                    userId: bid.UserId,
                    amount: bid.Amount,
                timestamp: bid.Timestamp);

                _bidEventPublisher.PublishNewHighestBid(newHighestBidEvent);

                return bid.Id;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.AuctionRoomId)
                    .NotEmpty().WithMessage("Auction Room ID is required.");

                RuleFor(x => x.UserId)
                    .NotEmpty().WithMessage("User ID is required.");

                RuleFor(x => x.Amount)
                    .GreaterThan(0).WithMessage("Bid amount must be greater than zero.");
            }
        }

    }
}
