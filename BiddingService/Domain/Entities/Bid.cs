namespace BiddingService.Domain.Entities
{
    public class Bid
    {
        public Guid Id { get; private set; }
        public Guid AuctionRoomId { get; private set; }
        public Guid UserId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Timestamp { get; private set; }

        public Bid(Guid auctionRoomId, Guid userId, decimal amount)
        {
            Id = Guid.NewGuid();
            AuctionRoomId = auctionRoomId;
            UserId = userId;
            Amount = amount;
            Timestamp = DateTime.UtcNow;
        }

       
        public void UpdateAmount(decimal newAmount)
        {
            if (newAmount <= Amount)
            {
                throw new ArgumentException("The new bid amount must be greater than the current amount.");
            }

            Amount = newAmount;
        }
    }

}
