using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameFlow;

namespace ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

public interface IInventoryService
{
    IReadOnlyList<GameEvent> EquipItem(Character player, int index);
}