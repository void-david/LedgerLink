using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace LedgerLink.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // 1. Register MediatR (Finds all commands in this project)
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // 2. Register Validators (Finds all validators in this project)
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
} 