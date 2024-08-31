using BiddingService.Domain.Entities;
using BiddingService.Infrastructure.Repositories;
using MediatR;

namespace BiddingService.Application.Queries
{
    public class GetBidByIdQuery
    {
        public record GetBidById(Guid Id) : IRequest<Bid>;

        public class QueryHandler : IRequestHandler<GetBidById, Bid>
        {
            private readonly IBidRepository _repository;

            public QueryHandler(IBidRepository repository)
            {
                _repository = repository;
            }

            public async Task<Bid> Handle(GetBidById request, CancellationToken cancellationToken)
            {
                var bid = await _repository.GetByIdAsync(request.Id);
                if (bid == null)
                {
                    throw new KeyNotFoundException("Bid not found.");
                }
                return bid;
            }
        }

    }
}
