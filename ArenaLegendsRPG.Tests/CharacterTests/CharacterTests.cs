using ArenaLegendsRPG.Core.Character;
using ArenaLegendsRPG.Core.Fighting;
using ArenaLegendsRPG.Core.Items;

namespace ArenaLegendsRPG.Tests.CharacterTests;

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
}