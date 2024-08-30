using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    namespace Shared.Events
    {
        public class StartAuctionEvent
        {
            public Guid AuctionRoomId { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
           
        }
    }

}
