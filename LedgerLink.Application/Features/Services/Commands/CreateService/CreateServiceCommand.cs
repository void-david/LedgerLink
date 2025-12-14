using MediatR;

namespace LedgerLink.Application.Features.Services.Commands.CreateService;

// Request returns id (int) of the new service

public record CreateServiceCommand(string Name, string Description, decimal BasePrice) : IRequest<int>;

