using InvoiceService.Application.Commands;
using InvoiceService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/v1/invoices")]
public class InvoiceController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvoiceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceCommand.CreateInvoice command)
    {
        var invoiceId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetInvoiceById), new { id = invoiceId }, null);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInvoiceById(Guid id)
    {
        var invoice = await _mediator.Send(new GetInvoiceByIdQuery.Query(id));
        if (invoice == null)
        {
            return NotFound("Invoice not found.");
        }

        return Ok(invoice);
    }
}
