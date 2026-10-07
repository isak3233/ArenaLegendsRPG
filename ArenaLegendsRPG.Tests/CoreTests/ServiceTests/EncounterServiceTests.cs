using ArenaLegendsRPG.Core.Encounter;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Core.Monster;

namespace ArenaLegendsRPG.Tests.CoreTests;

public class EncounterServiceTests
{
    [Fact]
    public void GenerateEncounter_WhenRollIsZero_ReturnsMonsterEncounter()
    {
        var random = new FakeRandomProvider(0, 0, 10, 5);
        var service = new EncounterService(random);

        var encounter = service.GenerateEncounter();

        Assert.IsType<MonsterEncounter>(encounter);
    }

    [Fact]
    public void GenerateEncounter_WhenRollIsOne_ReturnsChestEncounter()
    {
        var random = new FakeRandomProvider(1);
        var service = new EncounterService(random);

        var encounter = service.GenerateEncounter();

        Assert.IsType<ChestEncounter>(encounter);
    }

    [Fact]
    public void GenerateEncounter_WhenRollIsTwo_ReturnsStructureEncounter()
    {
        var random = new FakeRandomProvider(2);
        var service = new EncounterService(random);

        var encounter = service.GenerateEncounter();

        Assert.IsType<StructureEncounter>(encounter);
    }
}