using MediatR;

namespace LedgerLink.Application.Features.Clients.Queries.GetClients;

public record GetClientsQuery : IRequest<List<ClientDto>>;