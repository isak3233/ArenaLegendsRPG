using ArenaLegendsRPG.API.Feature.Healthy;
using ArenaLegendsRPG.Infrastructure;
using ArenaLegendsRPG.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ArenaLegendsRPG.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddInfrastructure(builder.Configuration);


        var app = builder.Build();
        
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ArenaLegendsDbContext>();
            dbContext.Database.Migrate();
        }
        
        app.MapHealthyEndpoint();

        app.Run();
    }
}