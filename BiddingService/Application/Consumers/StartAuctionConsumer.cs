namespace BiddingService.Application.Consumers
{
    using BiddingService.Domain.Entities;
    using BiddingService.Infrastructure.Repositories;
    using MassTransit;
    using Shared.Events.Shared.Events;

    public class StartAuctionConsumer : IConsumer<StartAuctionEvent>
    {
        private readonly IBidRepository _bidRepository; 
        private readonly ILogger<StartAuctionConsumer> _logger;

        public StartAuctionConsumer(IBidRepository bidRepository, ILogger<StartAuctionConsumer> logger)
        {
            _bidRepository = bidRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<StartAuctionEvent> context)
        {
            var auctionEvent = context.Message;

            _logger.LogInformation($"Auction Started: {auctionEvent.AuctionRoomId}");

            // Initialize bids for the auction
                  var initialBid = new Bid(
                   auctionRoomId: auctionEvent.AuctionRoomId,
                   userId: Guid.Empty,
                   amount: 0);
              

            await _bidRepository.AddAsync(initialBid);

            _logger.LogInformation($"Initial bid created for Auction: {auctionEvent.AuctionRoomId}");

            await Task.CompletedTask;
        }
    }



}
