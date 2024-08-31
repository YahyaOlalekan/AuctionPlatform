using MediatR;
using RoomService.Domain.Entities;
using RoomService.Infrastructure.Repositories;

namespace RoomService.Application.Queries
{
    public class GetAllAuctionRoomsQuery
    {
        public record GetAllAuctionRooms : IRequest<IEnumerable<AuctionRoom>>;

        public class QueryHandler : IRequestHandler<GetAllAuctionRooms, IEnumerable<AuctionRoom>>
        {
            private readonly IAuctionRoomRepository _repository;

            public QueryHandler(IAuctionRoomRepository repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<AuctionRoom>> Handle(GetAllAuctionRooms request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();
            }
        }

    }
}
