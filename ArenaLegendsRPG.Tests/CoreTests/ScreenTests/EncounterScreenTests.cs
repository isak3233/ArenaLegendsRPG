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
    private const int MonsterEncounterRoll = 0;
    private const int ChestEncounterRoll = 1;
    private const int StructureEncounterRoll = 2;
    private const int FleeSucceedsRoll = 0;
    private const int FleeFailsRoll = 99;
    private const int NoDropRoll = 99;
    private const int ChestHoldsFirstItem = 0;

    private const int KillsAnyMonster = 1000;
    private const int DoesNoDamage = 0;

    private readonly GameSession _session = new();
    private readonly StubGameScreen _gameMenu = new StubGameScreen(GameState.GameMenu);
    private readonly StubGameScreen _gameOver = new StubGameScreen(GameState.GameOver);
    private readonly StubScreenFactory _factory;

    public EncounterScreenTests()
    {
        _factory = new StubScreenFactory(gameMenu: _gameMenu, gameOver: _gameOver);
    }

    private EncounterScreen CreateSut(Character player, int encounterRoll, int fleeRoll = FleeSucceedsRoll, int lootRoll = NoDropRoll)
    {
        _session.SetPlayer(player);

        
        var encounterRolls = new StubRandomProvider(encounterRoll, 0, 0, 0);

        return new EncounterScreen(
            _session,
            _factory,
            new EncounterService(encounterRolls, new LootService(new StubRandomProvider(lootRoll))),
            new CombatService(new StubRandomProvider(fleeRoll)),
            new RewardService(new LevelingService()));
    }

    private EncounterScreen CreateMonsterSut(Character player, int fleeRoll = FleeSucceedsRoll)
    {
        return CreateSut(player, MonsterEncounterRoll, fleeRoll);
    }

    private EncounterScreen CreateChestSut(Character player)
    {
        return CreateSut(player, ChestEncounterRoll, lootRoll: ChestHoldsFirstItem);
    }

    private EncounterScreen CreateStructureSut(Character player)
    {
        return CreateSut(player, StructureEncounterRoll);
    }

    private static Character CreatePlayer(int health = 100, int attackDamage = DoesNoDamage)
    {
        return new Character("Hero", health, attackDamage, 0, 0, 0);
    }


    [Fact]
    public void GetAvailableActions_Monster_AttackAndFlee()
    {
        var sut = CreateMonsterSut(CreatePlayer());

        Assert.Equal(new[] { MenuAction.Attack, MenuAction.Flee }, sut.GetAvailableActions());
    }

    [Fact]
    public void GetAvailableActions_Chest_OpenChestAndLeave()
    {
        var sut = CreateChestSut(CreatePlayer());

        Assert.Equal(new[] { MenuAction.OpenChest, MenuAction.LeaveEncounter }, sut.GetAvailableActions());
    }

    [Fact]
    public void GetAvailableActions_Structure_OnlyLeave()
    {
        var sut = CreateStructureSut(CreatePlayer());

        Assert.Equal(new[] { MenuAction.LeaveEncounter }, sut.GetAvailableActions());
    }


    [Fact]
    public void Attack_MonsterDies_GoesToGameMenu()
    {
        var sut = CreateMonsterSut(CreatePlayer(attackDamage: KillsAnyMonster));

        var result = sut.Choose(MenuAction.Attack);

        Assert.Same(_gameMenu, result.Next);
        Assert.Contains(result.Events, e => e is DamageDealt);
    }

    [Fact]
    public void Attack_MonsterDies_IncludesRewardEvents()
    {
        var sut = CreateMonsterSut(CreatePlayer(attackDamage: KillsAnyMonster));

        var result = sut.Choose(MenuAction.Attack);

        Assert.Contains(result.Events, e => e is XpGained);
    }

    [Fact]
    public void Attack_PlayerDies_GoesToGameOver()
    {
        var sut = CreateMonsterSut(CreatePlayer(health: 0));

        var result = sut.Choose(MenuAction.Attack);

        Assert.Same(_gameOver, result.Next);
        Assert.Contains(result.Events, e => e is DamageDealt);
    }

    [Fact]
    public void Attack_NobodyDies_StaysOnSameScreenWithoutRewardEvents()
    {
        var sut = CreateMonsterSut(CreatePlayer());

        var result = sut.Choose(MenuAction.Attack);

        Assert.Same(sut, result.Next);
        Assert.Contains(result.Events, e => e is DamageDealt);
        Assert.DoesNotContain(result.Events, e => e is XpGained);
    }


    [Fact]
    public void Flee_Succeeds_GoesToGameMenu()
    {
        var sut = CreateMonsterSut(CreatePlayer(), FleeSucceedsRoll);

        var result = sut.Choose(MenuAction.Flee);

        Assert.Same(_gameMenu, result.Next);
        Assert.Contains(result.Events, e => e is FledFromEncounter);
    }

    [Fact]
    public void Flee_Fails_StaysOnSameScreen()
    {
        var sut = CreateMonsterSut(CreatePlayer(), FleeFailsRoll);

        var result = sut.Choose(MenuAction.Flee);

        Assert.Same(sut, result.Next);
    }


    [Fact]
    public void OpenChest_GoesToGameMenuWithItemFoundEvent()
    {
        var sut = CreateChestSut(CreatePlayer());

        var result = sut.Choose(MenuAction.OpenChest);

        Assert.Same(_gameMenu, result.Next);
        Assert.Contains(result.Events, e => e is ItemFound);
    }

    [Fact]
    public void LeaveChest_GoesToGameMenuWithoutItemFoundEvent()
    {
        var sut = CreateChestSut(CreatePlayer());

        var result = sut.Choose(MenuAction.LeaveEncounter);

        Assert.Same(_gameMenu, result.Next);
        Assert.DoesNotContain(result.Events, e => e is ItemFound);
    }

    [Fact]
    public void LeaveStructure_GoesToGameMenu()
    {
        var sut = CreateStructureSut(CreatePlayer());

        var result = sut.Choose(MenuAction.LeaveEncounter);

        Assert.Same(_gameMenu, result.Next);
    }
}