using MediatR;
using RoomService.Domain.Entities;
using RoomService.Infrastructure.Repositories;

namespace RoomService.Application.Queries
{
    public class GetAuctionRoomByIdQuery
    {
        public record Query(Guid Id) : IRequest<AuctionRoom>;

        public class QueryHandler : IRequestHandler<Query, AuctionRoom>
        {
            private readonly IAuctionRoomRepository _repository;

            public QueryHandler(IAuctionRoomRepository repository)
            {
                _repository = repository;
            }

            public async Task<AuctionRoom> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository.GetByIdAsync(request.Id);
            }
        }

    }
}
