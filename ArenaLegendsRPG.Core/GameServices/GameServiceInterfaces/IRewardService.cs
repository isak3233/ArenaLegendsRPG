using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.Encounters;
using ArenaLegendsRPG.Core.GameFlow;

namespace ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

public interface IRewardService
{
    IReadOnlyList<GameEvent> GrantMonsterRewards(Character player, MonsterEncounter encounter);
    IReadOnlyList<GameEvent> GrantChestLoot(Character player, ChestEncounter encounter);
}