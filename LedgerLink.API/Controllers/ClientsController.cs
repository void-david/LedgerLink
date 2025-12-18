using LedgerLink.Application.Features.Clients.Commands.CreateClient;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.Metrics;
using LedgerLink.Application.Features.Clients.Queries.GetClients;

namespace LedgerLink.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ClientDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetClientsQuery());
        return Ok(result);   
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateClientCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    
}