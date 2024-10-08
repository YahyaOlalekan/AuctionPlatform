﻿using MediatR;
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
        public async Task<IActionResult> Create([FromBody] CreateAuctionRoomCommand.CreateAuctionRoom command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var room = await _mediator.Send(new GetAuctionRoomByIdQuery.GetAuctionRoomById(id));
            if (room == null)
                return NotFound();
            return Ok(room);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAuctionRoomCommand.UpdateAuctionRoom command)
        {
            if (id != command.Id)
                return BadRequest("ID in the URL does not match the ID in the body.");

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteAuctionRoomCommand.DeleteAuctionRoom(id));
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuctionRooms()
        {
            var auctionRooms = await _mediator.Send(new GetAllAuctionRoomsQuery.GetAllAuctionRooms());
            return Ok(auctionRooms);
        }

        
    }


}
