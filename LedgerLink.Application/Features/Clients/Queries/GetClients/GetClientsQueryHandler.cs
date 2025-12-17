using LedgerLink.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LedgerLink.Application.Features.Clients.Queries.GetClients;

public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, List<ClientDto>>
{
    private readonly IApplicationDbContext _context;

    public GetClientsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        // 1. Get clients from DB
        // 2. Map them to Dto to avoid querying more columns

        return await _context.Clients
            .AsNoTracking()
            .Select(c => new ClientDto
            {
                Id = c.Id,
                Name = c.Name,
                Rfc = c.Rfc,
                Email = c.ContactEmail
            })
            .ToListAsync(cancellationToken);

    }

}