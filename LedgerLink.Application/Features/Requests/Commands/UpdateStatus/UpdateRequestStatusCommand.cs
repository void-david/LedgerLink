using LedgerLink.Domain.Enums;
using MediatR;

namespace LedgerLink.Application.Features.Requests.Commands.UpdateStatus;

public record UpdateRequestStatusCommand(int Id, RequestStatus NewStatus) : IRequest;