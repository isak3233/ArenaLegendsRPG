using ArenaLegendsRPG.Core.Fighting;
using System.Reflection.Metadata;

namespace ArenaLegendsRPG.Core.Monster;

public interface IMonster
{
    string Name { get; }
    int Level { get; }
    int Health { get; }
    int MaxHealth { get; }
    int AttackResist { get; }
    int MagicResist { get; }
    int AttackDamage { get; }
    int MagicDamage { get; }
    bool IsDead { get; }
    int XpReward { get; }
    public int TakeDamage(Damage damage);
}