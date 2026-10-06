using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ArenaLegendsRPG.Infrastructure.Data;

namespace ArenaLegendsRPG.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Missing connection string 'Default'.");

        services.AddDbContext<ArenaLegendsDbContext>(options => options.UseSqlite(connectionString));

        return services;
    }
}