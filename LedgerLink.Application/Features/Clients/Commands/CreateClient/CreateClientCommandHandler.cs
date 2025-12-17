using LedgerLink.Domain.Entities;
using LedgerLink.Application.Common.Interfaces;
using MediatR;

namespace LedgerLink.Application.Features.Clients.Commands.CreateClient;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateClientCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var entity = new Client
        {
            Name =request.Name,
            Rfc = request.Rfc,
            ContactEmail = request.ContactEmail,
            PhoneNumber = request.PhoneNumber,
            UserId = null,
        };
        _context.Clients.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        
        return entity.Id;
    }
}
