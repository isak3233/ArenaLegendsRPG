using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Core.GameFlow.Screens;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests.ScreenTests;

public class EncounterScreenTests
{
    private readonly GameSession _session = new();
    private readonly StubGameScreen _gameMenu = new StubGameScreen(GameState.GameMenu);
    private readonly StubGameScreen _gameOver = new StubGameScreen(GameState.GameOver);
    private readonly StubScreenFactory _factory;

    public EncounterScreenTests()
    {
        _factory = new StubScreenFactory(gameMenu: _gameMenu, gameOver: _gameOver);
    }

    private EncounterScreen CreateSut(
        Character player,
        int encounterChance = 0,
        int fleeChance = 0,
        int monsterType = 0,
        int monsterAttackDamage = 0,
        int monsterMagicDamage = 0,
        int[]? lootRolls = null)
    {
        _session.SetPlayer(player);
        var lootService = new LootService(new StubRandomProvider(lootRolls ?? new[] { 99 }));
        return new EncounterScreen(
            _session,
            _factory,
            new EncounterService(new StubRandomProvider(encounterChance, monsterType, monsterAttackDamage, monsterMagicDamage), lootService),
            new CombatService(new StubRandomProvider(fleeChance)),
            new RewardService(new LevelingService()));
    }

    private static Character CreatePlayer(int health = 100, int attackDamage = 0)
    {
        return new Character("Hero", health, attackDamage, 0, 0, 0);
    }


    [Fact]
    public void Attack_MonsterDies_GoesToGameMenu()
    {
        var sut = CreateSut(CreatePlayer(attackDamage: 1000));

        var result = sut.Choose(MenuAction.Attack);

        Assert.Same(_gameMenu, result.Next);
        Assert.Contains(result.Events, e => e is DamageDealt);
    }

    [Fact]
    public void Attack_PlayerDies_GoesToGameOver()
    {
        var sut = CreateSut(CreatePlayer(health: 0, attackDamage: 0));

        var result = sut.Choose(MenuAction.Attack);

        Assert.Same(_gameOver, result.Next);
        Assert.Contains(result.Events, e => e is DamageDealt);
    }

    [Fact]
    public void Attack_NobodyDies_StaysOnSameScreen()
    {
        var sut = CreateSut(CreatePlayer(health: 100, attackDamage: 0));

        var result = sut.Choose(MenuAction.Attack);

        Assert.Same(sut, result.Next);
        Assert.Contains(result.Events, e => e is DamageDealt);
    }

    [Fact]
    public void Attack_MonsterDies_GivesXpToPlayer()
    {
        var player = CreatePlayer(attackDamage: 1000);
        var sut = CreateSut(player, monsterAttackDamage: 8, monsterMagicDamage: 2); // goblin: 30 + (8 + 2) * 5 = 80 xp

        var result = sut.Choose(MenuAction.Attack);

        Assert.Equal(80, player.Xp);
        Assert.Contains(result.Events, e => e is XpGained gained && gained.Amount == 80);
    }

    [Fact]
    public void Attack_MonsterDiesWithEnoughXp_LevelsUpPlayer()
    {
        var player = CreatePlayer(attackDamage: 1000);
        var sut = CreateSut(player, monsterAttackDamage: 10, monsterMagicDamage: 10); // 30 + 100 = 130 xp

        var result = sut.Choose(MenuAction.Attack);

        Assert.Equal(2, player.Level);
        Assert.Contains(result.Events, e => e is LeveledUp);
    }

    [Fact]
    public void Attack_MonsterSurvives_GivesNoXp()
    {
        var player = CreatePlayer(attackDamage: 0);
        var sut = CreateSut(player);

        var result = sut.Choose(MenuAction.Attack);

        Assert.Equal(0, player.Xp);
        Assert.DoesNotContain(result.Events, e => e is XpGained);
    }

    [Fact]
    public void Attack_MonsterDiesAndDrops_AddsItemToInventory()
    {
        var player = CreatePlayer(attackDamage: 1000);
        var sut = CreateSut(player, lootRolls: new[] { 10, 0 }); // 10 = drop, 0 = Rusty Sword

        var result = sut.Choose(MenuAction.Attack);

        Assert.Equal("Rusty Sword", Assert.Single(player.Inventory.GetItems()).Name);
        Assert.Contains(result.Events, e => e is ItemFound found && found.ItemName == "Rusty Sword");
    }

    [Fact]
    public void Attack_MonsterDiesWithoutDrop_InventoryStaysEmpty()
    {
        var player = CreatePlayer(attackDamage: 1000);
        var sut = CreateSut(player, lootRolls: new[] { 99 });

        var result = sut.Choose(MenuAction.Attack);

        Assert.Empty(player.Inventory.GetItems());
        Assert.DoesNotContain(result.Events, e => e is ItemFound);
    }

    [Fact]
    public void Attack_MonsterSurvives_NeverDrops()
    {
        var player = CreatePlayer(attackDamage: 0);
        var sut = CreateSut(player, lootRolls: new[] { 10, 0 }); // skulle droppa om monstret dog

        sut.Choose(MenuAction.Attack);

        Assert.Empty(player.Inventory.GetItems());
    }


    [Fact]
    public void Flee_Succeeds_GoesToGameMenu()
    {
        var sut = CreateSut(CreatePlayer());

        var result = sut.Choose(MenuAction.Flee);

        Assert.Same(_gameMenu, result.Next);
        Assert.Contains(result.Events, e => e is FledFromEncounter);
    }

    [Fact]
    public void Flee_Fails_StaysOnSameScreen()
    {
        var sut = CreateSut(CreatePlayer(), fleeChance: 99);

        var result = sut.Choose(MenuAction.Flee);

        Assert.Same(sut, result.Next);
    }


    [Fact]
    public void OpenChest_AddsLootToInventory_AndGoesToGameMenu()
    {
        var player = CreatePlayer();
        var sut = CreateSut(player, encounterChance: 1, lootRolls: new[] { 0 }); // 1 = kista, 0 = Rusty Sword

        var result = sut.Choose(MenuAction.OpenChest);

        Assert.Equal("Rusty Sword", Assert.Single(player.Inventory.GetItems()).Name);
        Assert.Same(_gameMenu, result.Next);
    }

    [Fact]
    public void OpenChest_ReturnsItemFoundEvent()
    {
        var player = CreatePlayer();
        var sut = CreateSut(player, encounterChance: 1, lootRolls: new[] { 0 });

        var result = sut.Choose(MenuAction.OpenChest);

        Assert.Contains(result.Events, e => e is ItemFound found && found.PlayerName == "Hero" && found.ItemName == "Rusty Sword");
    }

    [Fact]
    public void LeaveChest_GoesToGameMenu_WithoutGivingItem()
    {
        var player = CreatePlayer();
        var sut = CreateSut(player, encounterChance: 1, lootRolls: new[] { 0 });

        var result = sut.Choose(MenuAction.LeaveEncounter);

        Assert.Same(_gameMenu, result.Next);
        Assert.Empty(player.Inventory.GetItems());
    }
    [Fact]
    public void GetAvailableActions_Monster_AttackAndFlee()
    {
        var sut = CreateSut(CreatePlayer());

        Assert.Equal(new[] { MenuAction.Attack, MenuAction.Flee }, sut.GetAvailableActions());
    }

    [Fact]
    public void GetAvailableActions_Chest_OpenChestAndLeave()
    {
        var sut = CreateSut(CreatePlayer(), encounterChance: 1, lootRolls: new[] { 0 });

        Assert.Equal(new[] { MenuAction.OpenChest, MenuAction.LeaveEncounter }, sut.GetAvailableActions());
    }

    [Fact]
    public void GetAvailableActions_Structure_OnlyLeave()
    {
        var sut = CreateSut(CreatePlayer(), encounterChance: 2);

        Assert.Equal(new[] { MenuAction.LeaveEncounter }, sut.GetAvailableActions());
    }

    [Fact]
    public void LeaveStructure_GoesToGameMenu()
    {
        var sut = CreateSut(CreatePlayer(), encounterChance: 2);

        var result = sut.Choose(MenuAction.LeaveEncounter);

        Assert.Same(_gameMenu, result.Next);
    }
}