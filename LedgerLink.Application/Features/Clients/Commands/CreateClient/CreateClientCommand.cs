using MediatR;

namespace LedgerLink.Application.Features.Clients.Commands.CreateClient;

// Request returns id (int) of the new service

public record CreateClientCommand(string Name, string Rfc, string ContactEmail, string PhoneNumber) : IRequest<int>;

