namespace ArenaLegendsRPG.Core.Items;

public class Weapon : Item
{
    public int AttackBonus { get; }
    public int MagicBonus { get; }

    public Weapon(string name, string description, int attackBonus = 0, int magicBonus = 0, ItemRarity rarity = ItemRarity.Common) : base(name, description, rarity)
    {
        AttackBonus = attackBonus;
        MagicBonus = magicBonus;
    }
}