using LedgerLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LedgerLink.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Service> Services { get; }
    DbSet<User> Users { get; }
    DbSet<Client> Clients { get; }
    DbSet<ServiceRequest> ServiceRequests { get; }
    DbSet<Document> Documents { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}


