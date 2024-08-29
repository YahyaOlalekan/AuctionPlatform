using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Commands;
using PaymentService.Application.Queries;

namespace PaymentService.API.Controllers
{
    [ApiController]
    [Route("api/v1/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentCommand.Command command)
        {
            var paymentId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPaymentById), new { id = paymentId }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(Guid id)
        {
            var payment = await _mediator.Send(new GetPaymentByIdQuery.Query(id));
            if (payment == null)
                return NotFound("Payment not found.");

            return Ok(payment);
        }
    }

}
