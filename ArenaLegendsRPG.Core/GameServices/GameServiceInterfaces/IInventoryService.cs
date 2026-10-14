using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameFlow;

namespace ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

public interface IInventoryService
{
    List<GameEvent> GetItemEvents(Character player);
    List<GameEvent> ToggleEquiped(Character player, int index);
}