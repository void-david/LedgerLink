using LedgerLink.Application.Common.Interfaces;
using LedgerLink.Domain.Entities;
using LedgerLink.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace LedgerLink.Application.Features.Requests.Commands.CreateRequest;

public class CreateServiceRequestCommandHandler : IRequestHandler<CreateServiceRequestCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateServiceRequestCommandHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<int> Handle(CreateServiceRequestCommand request, CancellationToken cancellationToken)
    {
        // 1. Get current user Id from the JWT token.
        var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString))
        {
            throw new UnauthorizedAccessException("Invalid User");
        }

        var userId = int.Parse(userIdString);

        // 2. Find the Client profile linked to this user
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
        if (client == null)
        {
            throw new Exception("Client profile not found.");
        }    

        // 3. Create a request
        var entity = new ServiceRequest
        {
            ClientId = client.Id,
            ServiceId = request.ServiceId,
            Status = RequestStatus.Pending,
            Notes = request.Notes,
            // CreatedAt is handled by BaseEntity
        };

        _context.ServiceRequests.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
        
    }
}
