using ArenaLegendsRPG.Core.Fighting;

namespace ArenaLegendsRPG.Core.Monster;

public abstract class MonsterBase : IMonster
{
    public int Health { get; private set; }
    public int MaxHealth { get; }
    public int AttackResist { get; }
    public int MagicResist { get; }

    public bool IsDead => Health <= 0;

    protected MonsterBase(int maxHealth, int attackResist, int magicResist)
    {
        MaxHealth = Health = maxHealth;
        AttackResist = attackResist;
        MagicResist = magicResist;
    }

    public int TakeDamage(Damage damage)
    {
        var actualDamage = DamageCalculator.Calculate(damage, AttackResist, MagicResist);
        Health = Math.Max(0, Health - actualDamage);
        return actualDamage;
    }
}