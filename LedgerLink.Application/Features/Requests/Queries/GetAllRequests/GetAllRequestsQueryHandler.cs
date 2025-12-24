using MediatR;
using LedgerLink.Application.Features.Requests.Dtos;
using LedgerLink.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LedgerLink.Application.Features.Requests.Queries.GetAllRequests;

public class GetAllRequestsQueryHandler : IRequestHandler<GetAllRequestsQuery, List<ServiceRequestDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllRequestsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ServiceRequestDto>> Handle(GetAllRequestsQuery request, CancellationToken cancellationToken)
    {
        return await _context.ServiceRequests
            .Include(r => r.Service)
            .Include(r => r.Client) // Need client info
            .Select(r => new ServiceRequestDto
            {
               Id = r.Id,
               ServiceName = r.Service!.Name,
               Price = r.Service.BasePrice,
               Status = r.Status.ToString(),
               CreatedAt = r.CreatedAt,
               ClientName = r.Client!.Name // New field
            })
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }
    
}