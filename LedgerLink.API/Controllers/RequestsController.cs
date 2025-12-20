using LedgerLink.Application.Features.Requests.Commands.CreateRequest;
using LedgerLink.Application.Features.Requests.Queries.GetMyRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    
}