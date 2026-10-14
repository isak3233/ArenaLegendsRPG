using ArenaLegendsRPG.API.Features.BattleLog;
using ArenaLegendsRPG.API.Features.Character;
using ArenaLegendsRPG.API.Features.Healthy;
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

        builder.Services.AddControllers()
            .AddJsonOptions(o => o.JsonSerializerOptions.AllowOutOfOrderMetadataProperties = true);
        builder.Services.AddScoped<ICharacterService, CharacterService>();
        builder.Services.AddScoped<IBattleLogService, BattleLogService>();

        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ArenaLegendsDbContext>();
            dbContext.Database.Migrate();
        }
        app.MapControllers();

        app.Run();
    }
}