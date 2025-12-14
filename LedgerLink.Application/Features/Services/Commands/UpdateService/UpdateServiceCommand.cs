using MediatR;

namespace LedgerLink.Application.Features.Services.Commands.UpdateService;

public record UpdateServiceCommand(int Id, string Name, string Description, decimal BasePrice) : IRequest;
