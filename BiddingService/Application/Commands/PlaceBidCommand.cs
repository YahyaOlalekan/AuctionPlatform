using FluentValidation;
using MediatR;
using BiddingService.Application.UnitOfWork;
using BiddingService.Infrastructure.Repositories;
using BiddingService.Domain.Entities;
using Shared.Events;
using MassTransit;

namespace BiddingService.Application.Commands
{
    public class PlaceBidCommand
    {
        public record PlaceBid(Guid AuctionRoomId, Guid UserId, decimal Amount) : IRequest<Guid>;

        public class CommandHandler : IRequestHandler<PlaceBid, Guid>
        {
            private readonly IBidRepository _bidRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IPublishEndpoint _publishEndpoint;
            public CommandHandler(IBidRepository bidRepository, IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
            {
                _bidRepository = bidRepository;
                _unitOfWork = unitOfWork;
                _publishEndpoint = publishEndpoint;
            }

            public async Task<Guid> Handle(PlaceBid request, CancellationToken cancellationToken)
            {
                var highestBid = await _bidRepository.GetHighestBidAsync(request.AuctionRoomId);

                if (highestBid != null && request.Amount <= highestBid.Amount)
                    throw new InvalidOperationException("Bid amount must be higher than the current highest bid.");

                var bid = new Bid(request.AuctionRoomId, request.UserId, request.Amount);
                await _bidRepository.AddAsync(bid);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // Publish the new highest bid event  
                var newHighestBidEvent = new NewHighestBidEvent{
                    AuctionRoomId = bid.AuctionRoomId,
                    UserId = bid.UserId,
                    Amount = bid.Amount,
                    Timestamp = bid.Timestamp};
                              
                await _publishEndpoint.Publish(newHighestBidEvent);

                return bid.Id;
            }
        }

        public class CommandValidator : AbstractValidator<PlaceBid>
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
