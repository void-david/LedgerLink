using LedgerLink.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LedgerLink.Application.Features.Services.Queries.GetServices;

public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, List<ServiceDto>>
{
    private readonly IApplicationDbContext _context;

    public GetServicesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ServiceDto>> Handle(GetServicesQuery request, CancellationToken cancellationToken)
    {
        // 1. Get services from DB
        // 2. Map them to Dto to avoid querying more columns
        
        return await _context.Services
            .AsNoTracking() //Performance optimizations
            .Select(s => new ServiceDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                BasePrice = s.BasePrice
            })
            .ToListAsync(cancellationToken);
    }
}