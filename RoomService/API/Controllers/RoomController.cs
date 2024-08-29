using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoomService.Application.Commands;
using RoomService.Application.Queries;

namespace RoomService.API.Controllers
{
    [ApiController]
    [Route("api/v1/rooms")]
    public class RoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuctionRoomCommand.Command command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var room = await _mediator.Send(new GetAuctionRoomByIdQuery.Query(id));
            if (room == null)
                return NotFound();
            return Ok(room);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAuctionRoomCommand.Command command)
        //{
        //    if (id != command.Id)
        //        return BadRequest("ID in the URL does not match the ID in the body.");

        //    await _mediator.Send(command);
        //    return NoContent(); 
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    await _mediator.Send(new DeleteAuctionRoomCommand.Command(id));
        //    return NoContent(); 
        //}
    }


}
