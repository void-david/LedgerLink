using MediatR;

namespace LedgerLink.Application.Features.Requests.Commands.CreateRequest;

public record CreateServiceRequestCommand(int ServiceId, string Notes) : IRequest<int>;