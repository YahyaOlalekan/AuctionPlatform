using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class NewHighestBidEvent
    {
        public Guid AuctionRoomId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
