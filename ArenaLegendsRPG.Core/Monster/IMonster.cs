using ArenaLegendsRPG.Core.Fighting;

namespace ArenaLegendsRPG.Core.Monster;

public interface IMonster
{
    int Health { get; }
    int MaxHealth { get; }
    int AttackResist { get; }
    int MagicResist { get; }

    public int TakeDamage(Damage damage);
}