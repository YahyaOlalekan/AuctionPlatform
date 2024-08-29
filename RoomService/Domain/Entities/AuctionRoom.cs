using Azure.Core;

namespace RoomService.Domain.Entities
{
    public class AuctionRoom
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public bool IsActive { get; private set; }

        private readonly List<Guid> _bids = new();
        public IReadOnlyCollection<Guid> Bids => _bids.AsReadOnly();

        public AuctionRoom(string name, DateTime startTime, DateTime endTime)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            StartTime = startTime;
            EndTime = endTime;
            IsActive = false;
        }
       

        public void StartAuction()
        {
            if (IsActive)
                throw new InvalidOperationException("Auction is already active.");
            IsActive = true;
        }

        public void EndAuction()
        {
            if (!IsActive)
                throw new InvalidOperationException("Auction is not active.");
            IsActive = false;
        }

        public void AddBid(Guid bidId)
        {
            if (!IsActive)
                throw new InvalidOperationException("Cannot add bid to inactive auction.");
            _bids.Add(bidId);
        }

        public void UpdateDetails(string name, DateTime startTime, DateTime endTime)
        {
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
        }
    }

}
