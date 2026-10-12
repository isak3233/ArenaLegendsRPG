using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;
using ArenaLegendsRPG.Core.RandomGen;
using ArenaLegendsRPG.UI.Rendering;
using ArenaLegendsRPG.UI.Rendering.EventRenders;
using ArenaLegendsRPG.UI.Rendering.Input;
using Microsoft.Extensions.DependencyInjection;

namespace ArenaLegendsRPG.UI;

public static class Program
{
    public static void Main()
    {
        var services = new ServiceCollection();

        services.AddScoped<IEventRender, CharacterCreationEventRender>();
        services.AddScoped<IEventRender, EncounterEventRender>();
        
        services.AddScoped<IGameEventRender, GameEventRender>();
        services.AddScoped<IGameEventInputRouter, GameEventInputRouter>();
        services.AddScoped<IInputHelper, InputHelper>();
        services.AddScoped<IConsoleIO, ConsoleIO>();
        
        services.AddSingleton<IGameSession, GameSession>();
        services.AddScoped<IRandomProvider, RandomProvider>();
        services.AddScoped<ICharacterCreationService, CharacterCreationService>();
        services.AddScoped<IEncounterService, EncounterService>();
        services.AddScoped<ICombatService, CombatService>();
        services.AddScoped<ILevelingService, LevelingService>();
        services.AddScoped<IScreenFactory, ScreenFactory>();
        services.AddScoped<IGameFlow, GameFlow>();

        services.AddScoped<ConsoleGameLoop>();

        using var provider = services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateOnBuild = true,    
            ValidateScopes = true
        });

        using var scope = provider.CreateScope();

        scope.ServiceProvider
            .GetRequiredService<ConsoleGameLoop>()
            .Run();
    }
}