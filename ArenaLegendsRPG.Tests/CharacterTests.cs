using ArenaLegendsRPG.Core.Character;
using ArenaLegendsRPG.Core.Items;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.Data;
using ArenaLegendsRPG.Core.Fighting;
public class CharacterTests
{
    private static Character CreateCharacter()
    {
        return new Character("Hero", health: 100, baseAttackDamage: 10, baseArmorDefence: 5, baseMagicResistance: 2);
    }
    private static Weapon CreateWeapon()
    {
        return new Weapon("Sword", "A sharp blade", 5);
    }
    private static Armor CreateArmor()
    {
        return new Armor("Armor", "A sturdy plate", 5);
    }
    [Fact]
    public void Constructor_SetsBaseStats()
    {
        var character = CreateCharacter();

        Assert.Equal("Hero", character.Name);
        Assert.Equal(100, character.Health);
        Assert.Equal(10, character.AttackDamage);
        Assert.Equal(5, character.ArmorDefence);
        Assert.Equal(2, character.MagicResistance);
    }


    [Fact]
    public void AddItem_AddsItemToInventory()
    {
        var character = CreateCharacter();
        var sword = CreateWeapon();

        character.Inventory.AddItem(sword);

        Assert.Contains(sword, character.Inventory.Items);
    }

    [Fact]
    public void EquipWeapon_ThrowsWhenWeaponNotInInventory()
    {
        var character = CreateCharacter();
        var sword = CreateWeapon();
        Assert.Throws<InvalidOperationException>(() => character.EquipWeapon(sword));
    }

    [Fact]
    public void EquipWeapon_SuccedsWhenWeaponIsInInventory()
    {
        var character = CreateCharacter();
        var sword = CreateWeapon();
        character.Inventory.AddItem(sword);

        character.EquipWeapon(sword);

        Assert.Equal(sword, character.EquippedWeapon);
        Assert.Equal(15, character.AttackDamage);
    }
    [Fact]
    public void EquipArmor_ThrowsWhenArmorNotInInventory()
    {
        var character = CreateCharacter();
        var armor = CreateArmor();

        Assert.Throws<InvalidOperationException>(() => character.EquipArmor(armor));


    }

    [Fact]
    public void EquipArmor_SuccedsWhenArmorIsInInventory()
    {
        var character = CreateCharacter();
        var armor = CreateArmor();
        character.Inventory.AddItem(armor);

        character.EquipArmor(armor);

        Assert.Equal(armor, character.EquippedArmor);
        Assert.Equal(10, character.ArmorDefence);

    }
    [Fact]
    public void TakeDamage_ReducesHealthByCalculatedAmount()
    {
        var character = CreateCharacter();
        var actual = character.TakeDamage(new Damage(20, DamageType.Physical));

        Assert.Equal(15, actual);
        Assert.Equal(85, character.Health);
    }
    [Fact]
    public void TakeDamage_HealthNeverGoesBelowZero()
    {
        var character = new Character("Hero", health: 10, baseAttackDamage: 10, baseArmorDefence: 0, baseMagicResistance: 0);

        character.TakeDamage(new Damage(100, DamageType.Physical));

        Assert.Equal(0, character.Health);
    }
}