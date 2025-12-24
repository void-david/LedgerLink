using System.Security.Claims;
using LedgerLink.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using LedgerLink.Application.Features.Requests.Dtos;

namespace LedgerLink.Application.Features.Requests.Queries.GetMyRequests;

public record GetMyRequestsQuery : IRequest<List<ServiceRequestDto>>;

public class GetMyRequestsQueryHandler : IRequestHandler<GetMyRequestsQuery, List<ServiceRequestDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetMyRequestsQueryHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<ServiceRequestDto>> Handle(GetMyRequestsQuery request, CancellationToken cancellationToken)
    {
        var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = int.Parse(userIdString!);

        // Filter the requests where Request -> Client -> UserID matches token
        return await _context.ServiceRequests
            .Include(r => r.Service)
            .Include(r => r.Client)
            .Where(r => r.Client!.UserId == userId)
            .Select(r => new ServiceRequestDto
            {
                Id = r.Id,
                ServiceName = r.Service!.Name,
                Price = r.Service.BasePrice,
                Status = r.Status.ToString(),
                CreatedAt = r.CreatedAt
            })
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}