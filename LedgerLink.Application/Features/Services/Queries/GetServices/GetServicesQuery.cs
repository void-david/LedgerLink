using MediatR;

namespace LedgerLink.Application.Features.Services.Queries.GetServices;

public record GetServicesQuery : IRequest<List<ServiceDto>>;