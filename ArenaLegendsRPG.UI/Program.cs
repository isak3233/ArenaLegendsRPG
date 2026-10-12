using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Core.RandomGen;

namespace ArenaLegendsRPG.UI;

public static class Program
{
    public static void Main()
    {
        var session = new GameSession();
        var randomProvider = new RandomProvider();
        var characterCreationService = new CharacterCreationService();
        var lootService = new LootService(randomProvider);
        var encounterService = new EncounterService(randomProvider, lootService);
        var combatService = new CombatService(randomProvider);
        var levelingService = new LevelingService();
        var rewardService = new RewardService(levelingService);
        var inventoryService = new InventoryService();
        var factory = new ScreenFactory(session, characterCreationService, encounterService, combatService, rewardService, inventoryService);
        var game = new GameFlow(factory);

        game.GetAvailableActions();
        Console.WriteLine("Hello, World!");
    }
}