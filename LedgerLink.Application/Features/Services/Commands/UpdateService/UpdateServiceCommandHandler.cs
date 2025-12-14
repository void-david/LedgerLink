using System.Security.Cryptography;
using LedgerLink.Application.Common.Interfaces;
using MediatR;

namespace LedgerLink.Application.Features.Services.Commands.UpdateService;

public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateServiceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Services.FindAsync(new object[] {request.Id}, cancellationToken);
        if (entity == null)
        {
            throw new Exception($"Service with ID {request.Id} not found.");
        }

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.BasePrice = request.BasePrice;

        await _context.SaveChangesAsync(cancellationToken);
        
    }
}