using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Core.GameFlow.Screens;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Core.Items;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests.ScreenTests;

public class InventoryScreenTests
{
    private readonly GameSession _session = new();
    private readonly Character _player = new("Hero", 100, 10, 0, 5, 2);
    private readonly StubGameScreen _gameMenu = new(GameState.GameMenu);
    private readonly InventoryScreen _sut;

    public InventoryScreenTests()
    {
        _session.SetPlayer(_player);
        var factory = new StubScreenFactory(gameMenu: _gameMenu);
        _sut = new InventoryScreen(_session, factory, new InventoryService());
    }

    [Fact]
    public void Select_EquipsTheItemAtIndex_AndStaysOnTheScreen()
    {
        var sword = new Weapon("Sword", "A sharp blade", attackBonus: 5);
        _player.Inventory.AddItem(sword);

        var result = _sut.Select(0);

        Assert.Same(sword, _player.EquippedWeapon);
        Assert.Same(_sut, result.Next);
        Assert.Contains(result.Events, e => e is ItemEquipped);
    }

    [Fact]
    public void Select_InvalidIndex_ReturnsInvalidItemSelectionEvent()
    {
        var result = _sut.Select(3);

        Assert.Same(_sut, result.Next);
        Assert.Contains(result.Events, e => e is InvalidItemSelection);
    }

    [Fact]
    public void Choose_Back_GoesToGameMenu()
    {
        var result = _sut.Choose(MenuAction.Back);

        Assert.Same(_gameMenu, result.Next);
    }

    [Fact]
    public void Choose_ActionNotInInventory_Throws()
    {
        Assert.Throws<InvalidOperationException>(() => _sut.Choose(MenuAction.Attack));
    }
}