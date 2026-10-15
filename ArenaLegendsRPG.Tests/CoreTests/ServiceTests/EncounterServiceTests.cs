using ArenaLegendsRPG.Core.Encounters;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Core.Monster;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests.ServiceTests;

public class EncounterServiceTests
{
    private const int PlayerLevelOne = 1;
    private const int MonsterEncounterRoll = 0;
    private const int ChestEncounterRoll = 86;
    private const int GoblinRoll = 0;
    private const int NoLevelOffset = 0;
    private const int OneLevelLower = -1;
    private const int OneLevelHigher = 1;
    private const int AttackRoll = 8;
    private const int MagicRoll = 2;

    private static int[] MonsterRolls(int levelOffset = NoLevelOffset)
    {
        return new[]
        {
            MonsterEncounterRoll,
            GoblinRoll,
            AttackRoll,
            MagicRoll,
            levelOffset
        };
    }

    private static EncounterService CreateSut(
        int[] encounterRolls,
        int[]? lootRolls = null)
    {
        var lootService = new LootService(
            new StubRandomProvider(lootRolls ?? new[] { 99 }));

        return new EncounterService(
            new StubRandomProvider(encounterRolls),
            lootService);
    }

    private static IMonster CreateMonster(
        int playerLevel,
        int levelOffset = NoLevelOffset)
    {
        var encounter = CreateSut(MonsterRolls(levelOffset))
            .GenerateEncounter(playerLevel);

        return Assert.IsType<MonsterEncounter>(encounter).Monster;
    }

    [Fact]
    public void GenerateEncounter_WhenRollIsZero_ReturnsMonsterEncounter()
    {
        var sut = CreateSut(MonsterRolls());

        var encounter = sut.GenerateEncounter(PlayerLevelOne);

        Assert.IsType<MonsterEncounter>(encounter);
    }

    [Fact]
    public void GenerateEncounter_WhenRollIsOne_ReturnsChestEncounter()
    {
        var sut = CreateSut(
            new[] { ChestEncounterRoll },
            lootRolls: new[] { 0, 0 });

        var encounter = sut.GenerateEncounter(PlayerLevelOne);

        Assert.IsType<ChestEncounter>(encounter);
    }

    [Fact]
    public void GenerateEncounter_Monster_CarriesDropWhenRollSucceeds()
    {
        var sut = CreateSut(
            MonsterRolls(),
            lootRolls: new[] { 10, 0, 0 });

        var encounter = sut.GenerateEncounter(PlayerLevelOne);

        var monsterEncounter = Assert.IsType<MonsterEncounter>(encounter);
        Assert.Equal("Rusty Sword", monsterEncounter.Drop?.Name);
    }

    [Fact]
    public void GenerateEncounter_Monster_HasNoDropWhenRollFails()
    {
        var sut = CreateSut(
            MonsterRolls(),
            lootRolls: new[] { 99 });

        var encounter = sut.GenerateEncounter(PlayerLevelOne);

        var monsterEncounter = Assert.IsType<MonsterEncounter>(encounter);
        Assert.Null(monsterEncounter.Drop);
    }

    [Fact]
    public void GenerateEncounter_Chest_ContainsLoot()
    {
        var sut = CreateSut(
            new[] { ChestEncounterRoll },
            lootRolls: new[] { 0, 0 });

        var encounter = sut.GenerateEncounter(PlayerLevelOne);

        var chest = Assert.IsType<ChestEncounter>(encounter);
        Assert.Equal("Rusty Sword", chest.Loot.Name);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(12)]
    public void GenerateEncounter_Monster_WithNoOffset_HasSameLevelAsPlayer(
        int playerLevel)
    {
        var monster = CreateMonster(playerLevel);

        Assert.Equal(playerLevel, monster.Level);
    }

    [Fact]
    public void GenerateEncounter_Monster_CanBeOneLevelAbovePlayer()
    {
        const int playerLevel = 5;

        var monster = CreateMonster(playerLevel, OneLevelHigher);

        Assert.Equal(playerLevel + 1, monster.Level);
    }

    [Fact]
    public void GenerateEncounter_Monster_CanBeOneLevelBelowPlayer()
    {
        const int playerLevel = 5;

        var monster = CreateMonster(playerLevel, OneLevelLower);

        Assert.Equal(playerLevel - 1, monster.Level);
    }

    [Fact]
    public void GenerateEncounter_Monster_NeverGetsLevelBelowOne()
    {
        var monster = CreateMonster(PlayerLevelOne, OneLevelLower);

        Assert.Equal(1, monster.Level);
    }

    [Fact]
    public void GenerateEncounter_Monster_AtLevelOne_UsesRolledDamageAsIs()
    {
        var monster = CreateMonster(PlayerLevelOne);

        Assert.Equal(AttackRoll, monster.AttackDamage);
        Assert.Equal(MagicRoll, monster.MagicDamage);
    }

    [Fact]
    public void GenerateEncounter_Monster_HigherLevelHasMoreHealth()
    {
        var weak = CreateMonster(PlayerLevelOne);
        var strong = CreateMonster(playerLevel: 6);

        Assert.True(strong.MaxHealth > weak.MaxHealth);
    }

    [Fact]
    public void GenerateEncounter_Monster_HigherLevelDealsMoreDamage()
    {
        var weak = CreateMonster(PlayerLevelOne);
        var strong = CreateMonster(playerLevel: 6);

        Assert.True(strong.AttackDamage > weak.AttackDamage);
        Assert.True(strong.MagicDamage > weak.MagicDamage);
    }

    [Fact]
    public void GenerateEncounter_Monster_HigherLevelHasMoreResist()
    {
        var weak = CreateMonster(PlayerLevelOne);
        var strong = CreateMonster(playerLevel: 6);

        Assert.True(strong.AttackResist > weak.AttackResist);
        Assert.True(strong.MagicResist > weak.MagicResist);
    }

    [Fact]
    public void GenerateEncounter_Monster_HigherLevelGivesMoreXp()
    {
        var weak = CreateMonster(PlayerLevelOne);
        var strong = CreateMonster(playerLevel: 6);

        Assert.True(strong.XpReward > weak.XpReward);
    }
}