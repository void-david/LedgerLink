using LedgerLink.Application.Common.Interfaces;
using MediatR;

namespace LedgerLink.Application.Features.Requests.Commands.UpdateStatus;

public class UpdateRequestStatusCommandHandler : IRequestHandler<UpdateRequestStatusCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateRequestStatusCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateRequestStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ServiceRequests.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null)
        {
            throw new Exception($"Request {request.Id} not found");
        }

        entity.Status = request.NewStatus;
        await _context.SaveChangesAsync(cancellationToken);
    }
}