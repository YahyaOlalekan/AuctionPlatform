using BiddingService.Domain.Entities;
using BiddingService.Infrastructure.Repositories;
using MediatR;

namespace BiddingService.Application.Queries
{
    public class GetHighestBidQuery
    {
        public record Query(Guid AuctionRoomId) : IRequest<Bid>;

        public class QueryHandler : IRequestHandler<Query, Bid>
        {
            private readonly IBidRepository _bidRepository;

            public QueryHandler(IBidRepository bidRepository)
            {
                _bidRepository = bidRepository;
            }

            public async Task<Bid> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _bidRepository.GetHighestBidAsync(request.AuctionRoomId);
            }
        }

    }
}
