using BiddingService.Domain.Entities;
using BiddingService.Infrastructure.Repositories;
using MediatR;

namespace BiddingService.Application.Queries
{
    public class GetBidsByAuctionRoomQuery
    {
        public record Query(Guid AuctionRoomId) : IRequest<List<Bid>>;

        public class RoomQueryHandler : IRequestHandler<Query, List<Bid>>
        {
            private readonly IBidRepository _bidRepository;

            public RoomQueryHandler(IBidRepository bidRepository)
            {
                _bidRepository = bidRepository;
            }

            public async Task<List<Bid>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _bidRepository.GetBidsByAuctionRoomAsync(request.AuctionRoomId);
            }
        }

    }
}
