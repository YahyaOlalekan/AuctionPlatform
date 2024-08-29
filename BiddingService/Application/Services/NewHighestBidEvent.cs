namespace BiddingService.Application.Services
{
    public class NewHighestBidEvent
    {
        public Guid AuctionRoomId { get; set; }
        public Guid BidId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }

        public NewHighestBidEvent(Guid auctionRoomId, Guid bidId, Guid userId, decimal amount, DateTime timestamp)
        {
            AuctionRoomId = auctionRoomId;
            BidId = bidId;
            UserId = userId;
            Amount = amount;
            Timestamp = timestamp;
        }
    }

}
