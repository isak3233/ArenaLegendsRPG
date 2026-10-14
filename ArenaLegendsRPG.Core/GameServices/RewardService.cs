using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.Encounters;
using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

namespace ArenaLegendsRPG.Core.GameServices;

public class RewardService : IRewardService
{
    private readonly ILevelingService _levelingService;

    public RewardService(ILevelingService levelingService)
    {
        _levelingService = levelingService;
    }

    public IReadOnlyList<GameEvent> GrantMonsterRewards(Character player, MonsterEncounter encounter)
    {
        var events = new List<GameEvent>();
        events.AddRange(_levelingService.GainXp(player, encounter.Monster.XpReward));

        if (encounter.Drop is not null)
        {
            player.Inventory.AddItem(encounter.Drop);
            events.Add(new ItemFound(player.Name, encounter.Drop.Name));
        }

        return events;
    }

    public IReadOnlyList<GameEvent> GrantChestLoot(Character player, ChestEncounter encounter)
    {
        player.Inventory.AddItem(encounter.Loot);

        return new GameEvent[] { new ItemFound(player.Name, encounter.Loot.Name) };
    }
}