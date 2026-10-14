using ArenaLegendsRPG.Core.APIHandlers;
using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;
using ArenaLegendsRPG.Core.RandomGen;
using ArenaLegendsRPG.UI.Rendering;
using ArenaLegendsRPG.UI.Rendering.EventRenders;
using ArenaLegendsRPG.UI.Rendering.Input;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArenaLegendsRPG.UI;

public static class Program
{
    public static async Task Main()
    {

        var services = new ServiceCollection();

        services.AddScoped<IEventRender, GameMenuEventRender>();
        services.AddScoped<IEventRender, CharacterCreationEventRender>();
        services.AddScoped<IEventRender, EncounterEventRender>();
        services.AddScoped<IEventRender, InventoryEventRender>();

        services.AddScoped<IGameEventRender, GameEventRender>();
        services.AddScoped<IGameEventInputRouter, GameEventInputRouter>();
        services.AddScoped<IInputHelper, InputHelper>();
        services.AddScoped<IConsoleIO, ConsoleIO>();


        services.AddScoped<ICharacterCreationService, CharacterCreationService>();
        services.AddScoped<IEncounterService, EncounterService>();
        services.AddScoped<ICombatService, CombatService>();
        services.AddScoped<ILevelingService, LevelingService>();
        services.AddScoped<ILootService, LootService>();
        services.AddScoped<IRewardService, RewardService>();
        services.AddScoped<IInventoryService, InventoryService>();

        services.AddScoped<ICharacterMapper, CharacterMapper>();

        services.AddSingleton<IGameSession, GameSession>();
        services.AddScoped<IRandomProvider, RandomProvider>();
        services.AddScoped<IScreenFactory, ScreenFactory>();
        services.AddScoped<IGameFlow, GameFlow>();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
        services.AddHttpClient<ICharacterRepository, ApiCharacterRepository>(client =>
            client.BaseAddress = new Uri(configuration["Api:BaseUrl"]!));

        services.AddScoped<ConsoleGameLoop>();

        using var provider = services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateOnBuild = true,
            ValidateScopes = true
        });

        using var scope = provider.CreateScope();

        await scope.ServiceProvider
            .GetRequiredService<ConsoleGameLoop>()
            .RunAsync();
    }
}