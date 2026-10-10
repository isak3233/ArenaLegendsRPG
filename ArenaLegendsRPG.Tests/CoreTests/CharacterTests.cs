
using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.Fighting;
using ArenaLegendsRPG.Core.Items;

namespace ArenaLegendsRPG.Tests.CoreTests;

public class CharacterTests
{
    private static Character CreateCharacter()
    {
        return new Character("Hero", 100, 10, 0, 5, 2);
    }

    private static Weapon CreateWeapon()
    {
        return new Weapon("Sword", "A sharp blade", 5, 3);
    }

    private static Protection CreateProtection()
    {
        return new Protection("Plate", "A sturdy plate", 5, 3);
    }

    [Fact]
    public void EquipWeapon_ThrowsWhenWeaponNotInInventory()
    {
        var character = CreateCharacter();
        var sword = CreateWeapon();

        Assert.Throws<InvalidOperationException>(() => character.EquipWeapon(sword));
    }

    [Fact]
    public void EquipWeapon_AddsWeaponBonusesToCharacterStats()
    {
        var character = CreateCharacter();
        var sword = CreateWeapon();
        character.Inventory.AddItem(sword);

        character.EquipWeapon(sword);

        Assert.Equal(15, character.AttackDamage);
        Assert.Equal(3, character.MagicDamage);
    }

    [Fact]
    public void EquipProtection_ThrowsWhenProtectionNotInInventory()
    {
        var character = CreateCharacter();
        var protection = CreateProtection();

        Assert.Throws<InvalidOperationException>(() => character.EquipProtection(protection));
    }

    [Fact]
    public void EquipProtection_AddsProtectionResistsToCharacterStats()
    {
        var character = CreateCharacter();
        var protection = CreateProtection();
        character.Inventory.AddItem(protection);

        character.EquipProtection(protection);

        Assert.Equal(10, character.AttackResist);
        Assert.Equal(5, character.MagicResist);
    }

    [Fact]
    public void TakeDamage_PhysicalDamage_IsReducedByAttackResist()
    {
        var character = CreateCharacter();

        var actual = character.TakeDamage(new Damage(20, DamageType.Physical));

        Assert.Equal(15, actual);
        Assert.Equal(85, character.Health);
    }

    [Fact]
    public void TakeDamage_MagicDamage_IsReducedByMagicResist()
    {
        var character = CreateCharacter();

        var actual = character.TakeDamage(new Damage(20, DamageType.Magic));

        Assert.Equal(18, actual);
        Assert.Equal(82, character.Health);
    }

    [Fact]
    public void TakeDamage_DamageExceedingHealth_HealthStopsAtZero()
    {
        var character = new Character("Hero", health: 10, baseAttackDamage: 10, baseMagicDamage: 10, baseAttackResist: 0, baseMagicResist: 0);

        character.TakeDamage(new Damage(100, DamageType.Physical));

        Assert.Equal(0, character.Health);
    }

    [Fact]
    public void Constructor_StartsAtLevelOneWithNoXp()
    {
        var character = CreateCharacter();

        Assert.Equal(1, character.Level);
        Assert.Equal(0, character.Xp);
        Assert.Equal(character.Health, character.MaxHealth);
    }

    [Fact]
    public void AddXp_IncreasesXp()
    {
        var character = CreateCharacter();

        character.AddXp(40);
        character.AddXp(10);

        Assert.Equal(50, character.Xp);
    }

    [Fact]
    public void AddXp_NegativeAmount_Throws()
    {
        var character = CreateCharacter();

        Assert.Throws<ArgumentOutOfRangeException>(() => character.AddXp(-1));
    }

    [Fact]
    public void LevelUp_SpendsXpAndIncreasesLevel()
    {
        var character = CreateCharacter();
        character.AddXp(130);

        character.LevelUp(xpRequired: 100);

        Assert.Equal(2, character.Level);
        Assert.Equal(30, character.Xp); 
    }

    [Fact]
    public void LevelUp_NotEnoughXp_Throws()
    {
        var character = CreateCharacter();
        character.AddXp(50);

        Assert.Throws<InvalidOperationException>(() => character.LevelUp(xpRequired: 100));
    }

    [Fact]
    public void IncreaseMaxHealth_RaisesMaxHealth()
    {
        var character = CreateCharacter();
        var before = character.MaxHealth;

        character.IncreaseMaxHealth(10);

        Assert.Equal(before + 10, character.MaxHealth);
    }

    [Fact]
    public void IncreaseBaseStats_RaisesAllBaseStats()
    {
        var character = CreateCharacter();
        var attack = character.BaseAttackDamage;
        var magic = character.BaseMagicDamage;
        var attackResist = character.BaseAttackResist;
        var magicResist = character.BaseMagicResist;

        character.IncreaseBaseStats(attackDamage: 2, magicDamage: 1, attackResist: 1, magicResist: 1);

        Assert.Equal(attack + 2, character.BaseAttackDamage);
        Assert.Equal(magic + 1, character.BaseMagicDamage);
        Assert.Equal(attackResist + 1, character.BaseAttackResist);
        Assert.Equal(magicResist + 1, character.BaseMagicResist);
    }

    [Fact]
    public void Heal_NeverExceedsMaxHealth()
    {
        var character = CreateCharacter();
        character.TakeDamage(new Damage(20, DamageType.Physical));

        character.Heal(1000);

        Assert.Equal(character.MaxHealth, character.Health);
    }
}