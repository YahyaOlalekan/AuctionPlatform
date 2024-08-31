using BiddingService.Domain.Entities;
using BiddingService.Infrastructure.Repositories;
using MediatR;

namespace BiddingService.Application.Queries
{
    public class GetAllBidsQuery
    {
        public record GetAllBids : IRequest<IEnumerable<Bid>>;

        public class QueryHandler : IRequestHandler<GetAllBids, IEnumerable<Bid>>
        {
            private readonly IBidRepository _repository;

            public QueryHandler(IBidRepository repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<Bid>> Handle(GetAllBids request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();
            }
        }

    }
}
