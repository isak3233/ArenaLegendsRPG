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
        int monsterMagicDamage = 0)
    {
        _session.SetPlayer(player);
        return new EncounterScreen(
            _session,
            _factory,
            new EncounterService(new StubRandomProvider(encounterChance, monsterType, monsterAttackDamage, monsterMagicDamage)),
            new CombatService(new StubRandomProvider(fleeChance)));
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
}