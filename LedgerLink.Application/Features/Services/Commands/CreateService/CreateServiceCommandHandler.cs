using LedgerLink.Domain.Entities;
using LedgerLink.Application.Common.Interfaces;
using MediatR;

namespace LedgerLink.Application.Features.Services.Commands.CreateService;

public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, int>
{
    private readonly IApplicationDbContext _context;
    
    public CreateServiceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var entity = new Service
        {
            Name = request.Name,
            Description = request.Description,
            BasePrice = request.BasePrice,
            IsActive = true
        };

        _context.Services.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        
        return entity.Id;
    }
}