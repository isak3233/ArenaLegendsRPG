using ArenaLegendsRPG.Core.Fighting;

namespace ArenaLegendsRPG.Core.Monster;

public interface IMonster
{
    int Health { get; }
    int MaxHealth { get; }
    int Armor { get; }
    int MagicResistance { get; }

    public int TakeDamage(Damage damage);
}