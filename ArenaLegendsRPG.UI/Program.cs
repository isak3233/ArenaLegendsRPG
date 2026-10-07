using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Core.RandomGen;

namespace ArenaLegendsRPG.UI;

class Program
{
    static void Main(string[] args)
    {
        var session = new GameSession();
        var randomProvider = new RandomProvider();
        var characterCreationService = new CharacterCreationService();
        var encounterService = new EncounterService(randomProvider);
        var combatService = new CombatService(randomProvider);
        var factory = new ScreenFactory(session, characterCreationService, encounterService, combatService);
        var game = new GameFlow(factory);

        Console.WriteLine("Hello, World!");
    }
}