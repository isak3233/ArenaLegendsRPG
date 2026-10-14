using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;
using ArenaLegendsRPG.Core.Items;

namespace ArenaLegendsRPG.Core.GameServices;

public class InventoryService : IInventoryService
{
    public List<GameEvent> GetItemEvents(Character player)
    {
        var items = player.Inventory.GetItems();
        var itemEvents = new List<GameEvent>();
        int index = 1;
        foreach (var item in items)
        {
            if (item is Weapon)
            {
                var weapon = (Weapon)item;
                bool isEquipped = false;
                if (weapon == player.EquippedWeapon) isEquipped = true;
                itemEvents.Add(new WeaponEvent(weapon.Name, weapon.Description, weapon.AttackBonus, weapon.MagicBonus, isEquipped, index));
            }

            if (item is Protection)
            {
                var protection = (Protection)item;
                bool isEquipped = false;
                if (protection == player.EquippedProtection) isEquipped = true;
                itemEvents.Add(new ProtectionEvent(protection.Name, protection.Description, protection.AttackResist, protection.MagicResist, isEquipped, index));
            }
            index++;
        }

        return itemEvents;


    }
    public List<GameEvent> ToggleEquiped(Character player, int index)
    {
        var events = new List<GameEvent>();
        var items = player.Inventory.GetItems();
        if (index < 0 || index >= items.Count)
        {
            events.Add(new InvalidItemSelection(index));
            return events;
        }

        var equiped = false;
        var item = items[index];
        switch (item)
        {
            case Weapon weapon:
                if (weapon == player.EquippedWeapon)
                {
                    player.UnEquipWeapon();
                }
                else
                {
                    equiped = true;
                    player.EquipWeapon(weapon);
                }
                break;
            case Protection protection:
                if (protection == player.EquippedProtection)
                {
                    player.UnEquipProtection();
                }
                else
                {
                    equiped = true;
                    player.EquipProtection(protection);
                }
                break;
            default:
                throw new InvalidOperationException($"{item.Name} cannot be equipped.");
        }
        events.Add(new ItemEquipped(player.Name, item.Name, equiped));
        return events;
    }
}