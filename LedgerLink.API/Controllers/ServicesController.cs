using LedgerLink.Application.Features.Services.Commands.CreateService;
using LedgerLink.Application.Features.Services.Commands.DeleteService;
using LedgerLink.Application.Features.Services.Commands.UpdateService;
using LedgerLink.Application.Features.Services.Queries.GetServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LedgerLink.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ServicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ServiceDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetServicesQuery());
        return Ok(result);
    }

    [HttpPost]
    [Authorize] // Route Locked
    public async Task<ActionResult<int>> Create(CreateServiceCommand command)
    {
        // Just throw the command at the meadiator
        // Don't know or care if it's handled, clean infrastructure baby!
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("{id}")]
    [Authorize] // Route Locked
    public async Task<ActionResult> Update(int id, UpdateServiceCommand command)
    {
        if (id != command.Id) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize] // Route Locked
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteServiceCommand(id));
        return NoContent();
    }


}