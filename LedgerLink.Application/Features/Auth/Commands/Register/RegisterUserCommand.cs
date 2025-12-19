using MediatR;

namespace LedgerLink.Application.Features.Auth.Commands.Register;

public record RegisterUserCommand(string Email, string Password, string FullName) : IRequest<int>;