namespace InvoiceService.Domain.Entities
{
    public class Invoice
    {
        public Guid Id { get; private set; }
        public Guid AuctionRoomId { get; private set; }
        public Guid BidId { get; private set; }
        public Guid UserId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Invoice(Guid auctionRoomId, Guid bidId, Guid userId, decimal amount)
        {
            Id = Guid.NewGuid();
            AuctionRoomId = auctionRoomId;
            BidId = bidId;
            UserId = userId;
            Amount = amount;
            CreatedAt = DateTime.UtcNow;
        }
    }

}
