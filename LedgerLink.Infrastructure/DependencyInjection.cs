using LedgerLink.Application.Common.Interfaces;
using LedgerLink.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;

namespace LedgerLink.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Get the connection string from appsettings.json
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<LedgerLinkDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IApplicationDbContext, LedgerLinkDbContext>();

        return services;
    }
}