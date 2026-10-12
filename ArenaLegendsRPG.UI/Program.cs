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

        services.AddSingleton<IGameEventRender, CharacterCreationEventRender>();
        
        services.AddSingleton<GameEventRender>();
        services.AddSingleton<GameEventInputRouter>();
        services.AddSingleton<InputHelper>();
        
        
        services.AddSingleton<IGameSession, GameSession>();
        services.AddSingleton<IRandomProvider, RandomProvider>();
        services.AddSingleton<ICharacterCreationService, CharacterCreationService>();
        services.AddSingleton<IEncounterService, EncounterService>();
        services.AddSingleton<ICombatService, CombatService>();
        services.AddSingleton<ILevelingService, LevelingService>();
        services.AddSingleton<IScreenFactory, ScreenFactory>();
        services.AddSingleton<IGameFlow, GameFlow>();

        services.AddSingleton<ConsoleGameLoop>();

        using var provider = services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateOnBuild = true,    
            ValidateScopes = true
        });

        provider.GetRequiredService<ConsoleGameLoop>().Run();
    }
}