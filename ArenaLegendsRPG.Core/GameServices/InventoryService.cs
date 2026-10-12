using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;
using ArenaLegendsRPG.Core.Items;

namespace ArenaLegendsRPG.Core.GameServices;

public class InventoryService : IInventoryService
{
    public IReadOnlyList<GameEvent> EquipItem(Character player, int index)
    {
        var items = player.Inventory.GetItems();
        if (index < 0 || index >= items.Count)
        {
            return new GameEvent[] { new InvalidItemSelection(index) };
        }

        var item = items[index];
        switch (item)
        {
            case Weapon weapon:
                player.EquipWeapon(weapon);
                break;
            case Protection protection:
                player.EquipProtection(protection);
                break;
            default:
                throw new InvalidOperationException($"{item.Name} cannot be equipped.");
        }

        return new GameEvent[] { new ItemEquipped(player.Name, item.Name) };
    }
}