using ArenaLegendsRPG.Core.Fighting;

namespace ArenaLegendsRPG.Core.Monster;

public abstract class MonsterBase : IMonster
{
    public int Health { get; private set; }
    public int MaxHealth { get; }
    public int Armor { get; }
    public int MagicResistance { get; }

    public bool IsDead => Health <= 0;

    protected MonsterBase(int maxHealth, int armor, int magicResistance)
    {
        MaxHealth = Health = maxHealth;
        Armor = armor;
        MagicResistance = magicResistance;
    }

    public int TakeDamage(Damage damage)
    {
        var actual = DamageCalculator.Calculate(damage, Armor, MagicResistance);
        Health = Math.Max(0, Health - actual);
        return actual;
    }
}