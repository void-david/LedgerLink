using LedgerLink.Application.Features.Requests.Dtos;
using MediatR;

namespace LedgerLink.Application.Features.Requests.Queries.GetAllRequests;

public record GetAllRequestsQuery : IRequest<List<ServiceRequestDto>>;

