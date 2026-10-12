using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;
using ArenaLegendsRPG.Core.Items;
using ArenaLegendsRPG.Core.Items.Interfaces;
using ArenaLegendsRPG.Core.RandomGen;

namespace ArenaLegendsRPG.Core.GameServices;

public class LootService : ILootService
{
    private const int ItemCount = 10;
    private const int MonsterDropChancePercent = 40;
    private readonly IRandomProvider _random;

    public LootService(IRandomProvider random)
    {
        _random = random;
    }

    public IItem? RollMonsterDrop()
    {
        var roll = _random.Next(0, 100);
        return roll < MonsterDropChancePercent ? GenerateLoot() : null;
    }

    public IItem GenerateLoot()
    {
        var index = _random.Next(0, ItemCount);
        return CreateItem(index);
    }

    private static IItem CreateItem(int index)
    {
        return index switch
        {
            0 => new Weapon("Rusty Sword", "A worn but serviceable blade.", attackBonus: 3),
            1 => new Weapon("Iron Sword", "A solid blade forged from iron.", attackBonus: 5),
            2 => new Weapon("Apprentice Staff", "A staff humming with weak magic.", magicBonus: 4),
            3 => new Protection("Leather Armor", "Light armor that turns aside blows.", attackResist: 2, magicResist: 0),
            4 => new Protection("Iron Plate", "Heavy plate that stops most blades.", attackResist: 4, magicResist: 1),
            5 => new Protection("Mage Robe", "A robe woven with protective runes.", attackResist: 1, magicResist: 3),
            6 => new Weapon("Steel Sword", "A well-balanced blade of folded steel.", attackBonus: 7),
            7 => new Weapon("Fire Staff", "A staff that glows with a restless flame.", magicBonus: 7),
            8 => new Protection("Chainmail", "Interlocked rings that shrug off cuts.", attackResist: 6, magicResist: 1),
            9 => new Protection("Enchanted Cloak", "A cloak that bends hostile magic away.", attackResist: 2, magicResist: 5),
            _ => throw new ArgumentOutOfRangeException(nameof(index), $"No item with index {index}.")
        };
    }
}