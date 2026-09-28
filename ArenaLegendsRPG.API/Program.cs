using ArenaLegendsRPG.API.Feature.Healthy;
using ArenaLegendsRPG.Infrastructure;
namespace ArenaLegendsRPG.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddInfrastructure(builder.Configuration);


        var app = builder.Build();
        
        app.MapHealthEndpoint();

        app.Run();
    }
}