
namespace ArenaLegendsRPG.Core.Items;

public class Weapon : Item
{
    public int? AttackBonus { get; }
    public int? MagicBonus { get; }

    public Weapon(string name, string description, int attackBonus = 0, int magicBonus = 0) : base(name, description)
    {
        AttackBonus = attackBonus;
        MagicBonus = magicBonus;
    }
}
