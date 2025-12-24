using LedgerLink.Application.Features.Requests.Commands.CreateRequest;
using LedgerLink.Application.Features.Requests.Queries.GetMyRequests;
using LedgerLink.Application.Features.Requests.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LedgerLink.Application.Features.Requests.Queries.GetAllRequests;
using LedgerLink.Application.Features.Requests.Commands.UpdateStatus;

namespace LedgerLink.API.Controllers;

[Authorize] // Secure access via login
[ApiController]
[Route("api/[controller]")]
public class RequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateServiceRequestCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpGet("my-requests")]
    public async Task<ActionResult<List<ServiceRequestDto>>> GetMyRequests()
    {
        var result = await _mediator.Send(new GetMyRequestsQuery());
        return Ok(result);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<ServiceRequestDto>>> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllRequestsQuery()));
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateStatus(int id, UpdateRequestStatusCommand command)
    {
        if (id != command.Id) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }
    
}