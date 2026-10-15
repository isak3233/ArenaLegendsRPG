using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.Encounters;
using ArenaLegendsRPG.Core.Fighting;
using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Core.GameFlow.Screens;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Core.Monster;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests.ScreenTests;

public class EncounterScreenTests
{
    private const int MonsterEncounterRoll = 0;
    private const int ChestEncounterRoll = 86;
    private const int FleeSucceedsRoll = 0;
    private const int FleeFailsRoll = 99;
    private const int NoDropRoll = 99;
    private const int ChestHoldsFirstItem = 0;

    private const int KillsAnyMonster = 1000;
    private const int DoesNoDamage = 0;

    private readonly GameSession _session = new();
    private readonly StubGameScreen _gameMenu =
        new(GameState.GameMenu);
    private readonly StubGameScreen _gameOver =
        new(GameState.GameOver);
    private readonly StubScreenFactory _factory;

    public EncounterScreenTests()
    {
        _factory = new StubScreenFactory(
            gameMenu: _gameMenu,
            gameOver: _gameOver);
    }

    private EncounterScreen CreateSut(
        Character player,
        int encounterRoll,
        int fleeRoll = FleeSucceedsRoll,
        int[]? lootRolls = null)
    {
        _session.SetPlayer(player);

        var encounterRolls = new StubRandomProvider(
            encounterRoll,
            0,
            0,
            0,
            0);

        var lootRandom = new StubRandomProvider(
            lootRolls ?? new[] { NoDropRoll });

        return new EncounterScreen(
            _session,
            _factory,
            new EncounterService(
                encounterRolls,
                new LootService(lootRandom)),
            new CombatService(
                new StubRandomProvider(fleeRoll),
                new StubBattleLogService()),
            new RewardService(new LevelingService()));
    }

    private EncounterScreen CreateMonsterSut(
        Character player,
        int fleeRoll = FleeSucceedsRoll)
    {
        return CreateSut(player, MonsterEncounterRoll, fleeRoll);
    }

    private EncounterScreen CreateChestSut(Character player)
    {
        return CreateSut(
            player,
            ChestEncounterRoll,
            lootRolls: new[] { ChestHoldsFirstItem, 0 });
    }

    private static Character CreatePlayer(
        int health = 100,
        int attackDamage = DoesNoDamage,
        int magicDamage = DoesNoDamage)
    {
        return new Character(
            "Hero",
            health,
            1,
            0,
            attackDamage,
            magicDamage,
            0,
            0);
    }

    [Fact]
    public async Task GetAvailableActions_Monster_OffersBothAttackTypesAndFlee()
    {
        var sut = CreateMonsterSut(CreatePlayer());

        await sut.ChooseAsync(MenuAction.BeginExplore);

        Assert.Equal(
            new[]
            {
                MenuAction.PhysicalAttack,
                MenuAction.MagicAttack,
                MenuAction.Flee
            },
            sut.GetAvailableActions());
    }

    [Fact]
    public async Task GetAvailableActions_Chest_OpenChestAndLeave()
    {
        var sut = CreateChestSut(CreatePlayer());

        await sut.ChooseAsync(MenuAction.BeginExplore);

        Assert.Equal(
            new[] { MenuAction.OpenChest, MenuAction.LeaveEncounter },
            sut.GetAvailableActions());
    }

    [Fact]
    public async Task PhysicalAttack_MonsterDies_IncludesDamageEvent()
    {
        var sut = CreateMonsterSut(
            CreatePlayer(attackDamage: KillsAnyMonster));

        await sut.ChooseAsync(MenuAction.BeginExplore);
        var result = await sut.ChooseAsync(MenuAction.PhysicalAttack);

        Assert.Same(sut, result.Next);
        Assert.Contains(
            result.Events.OfType<DamageDealt>(),
            damage => damage.Type == DamageType.Physical);
    }

    [Fact]
    public async Task MagicAttack_ReportsMagicDamageType()
    {
        var sut = CreateMonsterSut(
            CreatePlayer(magicDamage: KillsAnyMonster));

        await sut.ChooseAsync(MenuAction.BeginExplore);
        var result = await sut.ChooseAsync(MenuAction.MagicAttack);

        Assert.Contains(
            result.Events.OfType<DamageDealt>(),
            damage => damage.Type == DamageType.Magic);
    }

    [Fact]
    public async Task PhysicalAttack_MonsterDies_IncludesRewardEvents()
    {
        var sut = CreateMonsterSut(
            CreatePlayer(attackDamage: KillsAnyMonster));

        await sut.ChooseAsync(MenuAction.BeginExplore);
        var result = await sut.ChooseAsync(MenuAction.PhysicalAttack);

        Assert.Contains(result.Events, e => e is XpGained);
    }

    [Fact]
    public async Task PhysicalAttack_PlayerDies_GoesToGameOver()
    {
        var sut = CreateMonsterSut(CreatePlayer(health: 0));

        await sut.ChooseAsync(MenuAction.BeginExplore);
        var result = await sut.ChooseAsync(MenuAction.PhysicalAttack);

        Assert.Same(_gameOver, result.Next);
        Assert.Contains(result.Events, e => e is DamageDealt);
    }

    [Fact]
    public async Task PhysicalAttack_NobodyDies_StaysOnSameScreenWithoutRewards()
    {
        var sut = CreateMonsterSut(CreatePlayer());

        await sut.ChooseAsync(MenuAction.BeginExplore);
        var result = await sut.ChooseAsync(MenuAction.PhysicalAttack);

        Assert.Same(sut, result.Next);
        Assert.Contains(result.Events, e => e is DamageDealt);
        Assert.DoesNotContain(result.Events, e => e is XpGained);
    }

    [Fact]
    public async Task Flee_Succeeds_GoesToContinueExploring()
    {
        var sut = CreateMonsterSut(
            CreatePlayer(),
            FleeSucceedsRoll);

        await sut.ChooseAsync(MenuAction.BeginExplore);
        var result = await sut.ChooseAsync(MenuAction.Flee);

        Assert.Same(sut, result.Next);
        Assert.Contains(result.Events, e => e is FledFromEncounter);
    }

    [Fact]
    public async Task Flee_Fails_StaysOnSameScreen()
    {
        var sut = CreateMonsterSut(
            CreatePlayer(),
            FleeFailsRoll);

        var result = await sut.ChooseAsync(MenuAction.Flee);

        Assert.Same(sut, result.Next);
    }

    [Fact]
    public async Task OpenChest_GoesToGameMenuWithItemFoundEvent()
    {
        var sut = CreateChestSut(CreatePlayer());

        await sut.ChooseAsync(MenuAction.BeginExplore);
        var result = await sut.ChooseAsync(MenuAction.OpenChest);

        Assert.Same(sut, result.Next);
        Assert.Contains(result.Events, e => e is ItemFound);
    }

    [Fact]
    public async Task LeaveChest_GoesToGameMenuWithoutItemFoundEvent()
    {
        var sut = CreateChestSut(CreatePlayer());

        var result = await sut.ChooseAsync(MenuAction.LeaveEncounterScreen);

        Assert.Same(_gameMenu, result.Next);
        Assert.DoesNotContain(result.Events, e => e is ItemFound);
    }

    [Fact]
    public async Task BeginExplore_Monster_AnnouncesMonsterStatsAndResistances()
    {
        var sut = CreateMonsterSut(CreatePlayer());

        var result = await sut.ChooseAsync(MenuAction.BeginExplore);

        var monster = Assert.IsType<MonsterEncounter>(
            sut.CurrentEncounter).Monster;

        var started = Assert.Single(
            result.Events.OfType<MonsterEncounterStarted>());

        Assert.Equal(monster.Name, started.Name);
        Assert.Equal(monster.Level, started.Level);
        Assert.Equal(monster.Health, started.Health);
        Assert.Equal(monster.AttackDamage, started.AttackDamage);
        Assert.Equal(monster.MagicDamage, started.MagicDamage);
        Assert.Equal(monster.AttackResist, started.AttackResist);
        Assert.Equal(monster.MagicResist, started.MagicResist);
    }
}