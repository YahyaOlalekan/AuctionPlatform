using BiddingService.Application.Commands;
using BiddingService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BiddingService.API.Controllers
{
    [ApiController]
    [Route("api/v1/bids")]
    public class BiddingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BiddingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceBid([FromBody] PlaceBidCommand.Command command)
        {
            var bidId = await _mediator.Send(command);
            return Ok(new { BidId = bidId });
        }

        [HttpGet("{auctionRoomId}/highest")]
        public async Task<IActionResult> GetHighestBid(Guid auctionRoomId)
        {
            var highestBid = await _mediator.Send(new GetHighestBidQuery.Query(auctionRoomId));
            if (highestBid == null)
                return NotFound("No bids found for this auction room.");

            return Ok(highestBid);
        }

        [HttpGet("{auctionRoomId}/bids")]
        public async Task<IActionResult> GetBids(Guid auctionRoomId)
        {
            var bids = await _mediator.Send(new GetBidsByAuctionRoomQuery.Query(auctionRoomId));
            if (bids == null || !bids.Any())
                return NotFound("No bids found for this auction room.");

            return Ok(bids);
        }
    }

}
