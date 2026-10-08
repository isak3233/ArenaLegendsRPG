using ArenaLegendsRPG.Core.Encounters;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests.ServiceTests;

public class EncounterServiceTests
{
    [Fact]
    public void GenerateEncounter_WhenRollIsZero_ReturnsMonsterEncounter()
    {
        var random = new StubRandomProvider(0, 0, 10, 5);
        var service = new EncounterService(random);

        var encounter = service.GenerateEncounter();

        Assert.IsType<MonsterEncounter>(encounter);
    }

    [Fact]
    public void GenerateEncounter_WhenRollIsOne_ReturnsChestEncounter()
    {
        var random = new StubRandomProvider(1);
        var service = new EncounterService(random);

        var encounter = service.GenerateEncounter();

        Assert.IsType<ChestEncounter>(encounter);
    }

    [Fact]
    public void GenerateEncounter_WhenRollIsTwo_ReturnsStructureEncounter()
    {
        var random = new StubRandomProvider(2);
        var service = new EncounterService(random);

        var encounter = service.GenerateEncounter();

        Assert.IsType<StructureEncounter>(encounter);
    }
}