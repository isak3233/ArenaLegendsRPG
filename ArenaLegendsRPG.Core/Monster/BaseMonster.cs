using ArenaLegendsRPG.Core.Fighting;

namespace ArenaLegendsRPG.Core.Monster;

public abstract class MonsterBase : IMonster
{
    public string Name { get; }
    public int Health { get; private set; }
    public int MaxHealth { get; }
    public int AttackResist { get; }
    public int MagicResist { get; }
    public int AttackDamage { get; }
    public int MagicDamage { get; }

    public bool IsDead => Health <= 0;
    public virtual int XpReward => MaxHealth + (AttackDamage + MagicDamage) * 5;

    protected MonsterBase(string name, int maxHealth, int attackResist, int magicResist, int attackDamage, int magicDamage)
    {
        Name = name;
        MaxHealth = Health = maxHealth;
        AttackResist = attackResist;
        MagicResist = magicResist;
        AttackDamage = attackDamage;
        MagicDamage = magicDamage;
    }

    public int TakeDamage(Damage damage)
    {
        var actualDamage = DamageCalculator.Calculate(damage, AttackResist, MagicResist);
        Health = Math.Max(0, Health - actualDamage);
        return actualDamage;
    }
}