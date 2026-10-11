using ArenaLegendsRPG.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ArenaLegendsRPG.IntegrationTests;

public class CustomWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("ConnectionStrings:Default", "DataSource=:memory:");

        builder.ConfigureServices(services =>
        {
            var options = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DbContextOptions<ArenaLegendsDbContext>));
            if (options != null) services.Remove(options);

            var optionsConfig = services.SingleOrDefault(d =>
                d.ServiceType == typeof(IDbContextOptionsConfiguration<ArenaLegendsDbContext>));
            if (optionsConfig != null) services.Remove(optionsConfig);

            var connection = services.SingleOrDefault(d =>
                d.ServiceType == typeof(SqliteConnection));
            if (connection != null) services.Remove(connection);

            services.AddSingleton<SqliteConnection>(_ =>
            {
                var c = new SqliteConnection("DataSource=:memory:");
                c.Open();
                return c;
            });

            services.AddDbContext<ArenaLegendsDbContext>((container, o) =>
                o.UseSqlite(container.GetRequiredService<SqliteConnection>()));
        });
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = base.CreateHost(builder);

        using var scope = host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ArenaLegendsDbContext>();
        db.Database.EnsureCreated();

        return host;
    }
}