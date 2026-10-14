using ArenaLegendsRPG.Core.Encounters;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests.ServiceTests;

public class EncounterServiceTests
{
    private static EncounterService CreateSut(int[] encounterRolls, int[]? lootRolls = null)
    {
        var lootService = new LootService(new StubRandomProvider(lootRolls ?? new[] { 99 }));
        return new EncounterService(new StubRandomProvider(encounterRolls), lootService);
    }

    [Fact]
    public void GenerateEncounter_WhenRollIsZero_ReturnsMonsterEncounter()
    {

        // encounter = monster, art = goblin, attackDamage = 8, magicDamage = 2
        var sut = CreateSut(new[] { 0, 0, 8, 2 });

        var random = new StubRandomProvider(0, 0, 0, 0);
        // var service = new EncounterService(random);


        var encounter = sut.GenerateEncounter();

        Assert.IsType<MonsterEncounter>(encounter);
    }

    [Fact]
    public void GenerateEncounter_WhenRollIsOne_ReturnsChestEncounter()
    {
        var sut = CreateSut(new[] { 86 }, lootRolls: new[] { 0 });

        var encounter = sut.GenerateEncounter();

        Assert.IsType<ChestEncounter>(encounter);
    }


    [Fact]
    public void GenerateEncounter_Monster_CarriesDropWhenRollSucceeds()
    {
        var sut = CreateSut(new[] { 0, 0, 8, 2 }, lootRolls: new[] { 10, 0 });

        var encounter = sut.GenerateEncounter();

        var monsterEncounter = Assert.IsType<MonsterEncounter>(encounter);
        Assert.Equal("Rusty Sword", monsterEncounter.Drop?.Name);
    }

    [Fact]
    public void GenerateEncounter_Monster_HasNoDropWhenRollFails()
    {
        var sut = CreateSut(new[] { 0, 0, 8, 2 }, lootRolls: new[] { 99 });

        var encounter = sut.GenerateEncounter();

        var monsterEncounter = Assert.IsType<MonsterEncounter>(encounter);
        Assert.Null(monsterEncounter.Drop);
    }

    [Fact]
    public void GenerateEncounter_Chest_ContainsLoot()
    {
        var sut = CreateSut(new[] { 86 }, lootRolls: new[] { 0 });

        var encounter = sut.GenerateEncounter();

        var chest = Assert.IsType<ChestEncounter>(encounter);
        Assert.Equal("Rusty Sword", chest.Loot.Name);
    }
}