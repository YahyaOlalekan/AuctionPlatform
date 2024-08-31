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
        public async Task<IActionResult> PlaceBid([FromBody] PlaceBidCommand.PlaceBid command)
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

        [HttpGet]
        public async Task<IActionResult> GetAllBids()
        {
            var bids = await _mediator.Send(new GetAllBidsQuery());
            return Ok(bids);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBidById(Guid id)
        {
            var bid = await _mediator.Send(new GetBidByIdQuery.GetBidById(id));
            if (bid == null)
                return NotFound();

            return Ok(bid);
        }

       

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBid(Guid id, [FromBody] UpdateBidCommand.UpdateBid command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBid(Guid id)
        {
            await _mediator.Send(new DeleteBidCommand.DeleteBid(id));
            return NoContent();
        }
    }

}
