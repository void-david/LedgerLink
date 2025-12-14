using LedgerLink.Application.Common.Interfaces;
using LedgerLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LedgerLink.Infrastructure.Persistence;

public class LedgerLinkDbContext : DbContext, IApplicationDbContext
{
    public LedgerLinkDbContext(DbContextOptions<LedgerLinkDbContext> options) : base(options)
    {    
    }
    
    // Database tables
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceRequest> ServiceRequests { get; set; }
    public DbSet<Document> Documents { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships and constraints here

        // Example: BasePrice should be a decimal with specific precision
        modelBuilder.Entity<Service>()
            .Property(s => s.BasePrice)
            .HasColumnType("decimal(18, 2)");

        // Example: One User has one Client profile
        modelBuilder.Entity<User>()
            .HasOne(u => u.ClientProfile)
            .WithOne(c => c.User)
            .HasForeignKey<Client>(c => c.UserId);

    }
    


}