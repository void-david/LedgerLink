using LedgerLink.Application.Features.Auth.Commands.Login;
using LedgerLink.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LedgerLink.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResult>> Login(LoginCommand command)
    {
        try 
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<int>> Register(RegisterUserCommand command)
    {
        try
        {
            var userId = await _mediator.Send(command);
            return Ok(userId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}