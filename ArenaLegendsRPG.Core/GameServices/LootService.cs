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

        if (roll >= MonsterDropChancePercent)
        {
            return null;
        }

        return GenerateLoot();
    }

    public IItem GenerateLoot()
    {
        var index = _random.Next(0, ItemCount);

        if (index < 0 || index >= ItemCount)
        {
            throw new ArgumentOutOfRangeException(
                nameof(index),
                index,
                $"No item with index {index}.");
        }

        var rarity = RollRarity();

        return CreateItem(index, rarity);
    }

    private ItemRarity RollRarity()
    {
        var roll = _random.Next(0, 100);

        return roll switch
        {
            < 70 => ItemRarity.Common,
            < 90 => ItemRarity.Rare,
            < 98 => ItemRarity.Epic,
            _ => ItemRarity.Legendary
        };
    }

    private static int ScaleStat(int value, ItemRarity rarity)
    {
        var multiplier = rarity switch
        {
            ItemRarity.Rare => 1.25,
            ItemRarity.Epic => 1.5,
            ItemRarity.Legendary => 2.0,
            _ => 1.0
        };

        return (int)Math.Ceiling(value * multiplier);
    }

    private static IItem CreateItem(int index, ItemRarity rarity)
    {
        return index switch
        {
            0 => new Weapon(
                "Rusty Sword",
                "A worn but serviceable blade.",
                attackBonus: ScaleStat(3, rarity),
                rarity: rarity),

            1 => new Weapon(
                "Iron Sword",
                "A solid blade forged from iron.",
                attackBonus: ScaleStat(5, rarity),
                rarity: rarity),

            2 => new Weapon(
                "Apprentice Staff",
                "A staff humming with weak magic.",
                magicBonus: ScaleStat(4, rarity),
                rarity: rarity),

            3 => new Protection(
                "Leather Armor",
                "Light armor that turns aside blows.",
                attackResist: ScaleStat(2, rarity),
                magicResist: 0,
                rarity: rarity),

            4 => new Protection(
                "Iron Plate",
                "Heavy plate that stops most blades.",
                attackResist: ScaleStat(4, rarity),
                magicResist: ScaleStat(1, rarity),
                rarity: rarity),

            5 => new Protection(
                "Mage Robe",
                "A robe woven with protective runes.",
                attackResist: ScaleStat(1, rarity),
                magicResist: ScaleStat(3, rarity),
                rarity: rarity),

            6 => new Weapon(
                "Steel Sword",
                "A well-balanced blade of folded steel.",
                attackBonus: ScaleStat(7, rarity),
                rarity: rarity),

            7 => new Weapon(
                "Fire Staff",
                "A staff that glows with a restless flame.",
                magicBonus: ScaleStat(7, rarity),
                rarity: rarity),

            8 => new Protection(
                "Chainmail",
                "Interlocked rings that shrug off cuts.",
                attackResist: ScaleStat(6, rarity),
                magicResist: ScaleStat(1, rarity),
                rarity: rarity),

            9 => new Protection(
                "Enchanted Cloak",
                "A cloak that bends hostile magic away.",
                attackResist: ScaleStat(2, rarity),
                magicResist: ScaleStat(5, rarity),
                rarity: rarity),

            _ => throw new ArgumentOutOfRangeException(
                nameof(index),
                index,
                $"No item with index {index}.")
        };
    }
}