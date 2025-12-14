using MediatR;

namespace LedgerLink.Application.Features.Auth.Commands.Login;

public record LoginResult(string Token, string Role, string Email);

public record LoginCommand(string Email, string Password) : IRequest<LoginResult>;