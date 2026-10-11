
using ArenaLegendsRPG.Core.Fighting;
using ArenaLegendsRPG.Core.Items;


namespace ArenaLegendsRPG.Core.Characters;


public class Character
{
    public string Name { get; }
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    public int Level { get; private set; } = 1;
    public int Xp { get; private set; }
    public int BaseAttackDamage { get; private set; }
    public int BaseMagicDamage { get; private set; }
    public int BaseAttackResist { get; private set; }
    public int BaseMagicResist { get; private set; }

    public Weapon? EquippedWeapon { get; private set; }
    public Protection? EquippedProtection { get; private set; }

    public int AttackDamage => BaseAttackDamage + (EquippedWeapon?.AttackBonus ?? 0);
    public int AttackResist => BaseAttackResist + (EquippedProtection?.AttackResist ?? 0);

    public int MagicDamage => BaseMagicDamage + (EquippedWeapon?.MagicBonus ?? 0);
    public int MagicResist => BaseMagicResist + (EquippedProtection?.MagicResist ?? 0);
    public Inventory Inventory { get; } = new();



    public Character(string name, int health, int baseAttackDamage, int baseMagicDamage, int baseAttackResist, int baseMagicResist)
    {
        Name = name;
        Health = health;
        MaxHealth = health;
        BaseAttackDamage = baseAttackDamage;
        BaseMagicDamage = baseMagicDamage;
        BaseAttackResist = baseAttackResist;
        BaseMagicResist = baseMagicResist;
    }


    public void EquipWeapon(Weapon weapon)
    {
        if (!Inventory.GetItems().Contains(weapon))
        {
            throw new InvalidOperationException("Cannot equip a weapon that is not in the inventory");
        }

        EquippedWeapon = weapon;
    }
    public void EquipProtection(Protection protection)
    {
        if (!Inventory.GetItems().Contains(protection))
        {
            throw new InvalidOperationException("Cannot equip armor that is not in the inventory");
        }
        EquippedProtection = protection;
    }

    public int TakeDamage(Damage damage)
    {
        var actualDamage = DamageCalculator.Calculate(damage, AttackResist, MagicResist);
        Health = Math.Max(0, Health - actualDamage);
        return actualDamage;
    }

    public void AddXp(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Xp cannot be negative.");
        }

        Xp += amount;
    }
    public void LevelUp(int xpRequired)
    {
        if (Xp < xpRequired)
        {
            throw new InvalidOperationException("Not enough xp to level up");
        }
        Xp -= xpRequired;
        Level++;
    }

    public void IncreaseMaxHealth(int amount)
    {
        MaxHealth += amount;
    }
    public void IncreaseBaseStats(int attackDamage, int magicDamage, int attackResist, int magicResist)
    {
        BaseAttackDamage += attackDamage;
        BaseMagicDamage += magicDamage;
        BaseAttackResist += attackResist;
        BaseMagicResist += magicResist;
    }

    public void Heal(int amount)
    {
        Health = Math.Min(MaxHealth, Health + amount);
    }
}



