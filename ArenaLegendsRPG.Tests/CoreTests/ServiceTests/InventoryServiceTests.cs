using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Core.Items;

namespace ArenaLegendsRPG.Tests.CoreTests.ServiceTests;

public class InventoryServiceTests
{
    private readonly InventoryService _sut = new();
    private readonly Character _player = new("Hero", 100, 0, 1, 10, 0, 5, 2);

    [Fact]
    public void EquipItem_Weapon_EquipsItAndReturnsItemEquipped()
    {
        var sword = new Weapon("Sword", "A sharp blade", attackBonus: 5);
        _player.Inventory.AddItem(sword);

        var events = _sut.EquipItem(_player, 0);

        Assert.Same(sword, _player.EquippedWeapon);
        Assert.Contains(events, e => e is ItemEquipped equipped && equipped.PlayerName == "Hero" && equipped.ItemName == "Sword");
    }

    [Fact]
    public void EquipItem_Protection_EquipsIt()
    {
        var plate = new Protection("Plate", "A sturdy plate", attackResist: 4, magicResist: 1);
        _player.Inventory.AddItem(plate);

        _sut.EquipItem(_player, 0);

        Assert.Same(plate, _player.EquippedProtection);
    }

    [Fact]
    public void EquipItem_SecondWeapon_ReplacesTheFirst()
    {
        var sword = new Weapon("Sword", "A sharp blade", attackBonus: 5);
        var axe = new Weapon("Axe", "A heavy axe", attackBonus: 8);
        _player.Inventory.AddItem(sword);
        _player.Inventory.AddItem(axe);

        _sut.EquipItem(_player, 0);
        _sut.EquipItem(_player, 1);

        Assert.Same(axe, _player.EquippedWeapon);
        Assert.Equal(18, _player.AttackDamage);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1)]
    [InlineData(99)]
    public void EquipItem_InvalidIndex_ReturnsInvalidItemSelectionAndEquipsNothing(int index)
    {
        _player.Inventory.AddItem(new Weapon("Sword", "A sharp blade", attackBonus: 5));

        var events = _sut.EquipItem(_player, index);

        Assert.Contains(events, e => e is InvalidItemSelection selection && selection.Index == index);
        Assert.Null(_player.EquippedWeapon);
    }
}