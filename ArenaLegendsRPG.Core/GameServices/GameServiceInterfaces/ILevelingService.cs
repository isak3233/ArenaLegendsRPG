using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameFlow;

namespace ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

public interface ILevelingService
{
    int XpRequiredForNextLevel(int level);
    IReadOnlyList<GameEvent> GainXp(Character character, int amount);
}