using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.UI;
using ArenaLegendsRPG.UI.Rendering.EventRenders;
using Moq;
using Xunit;

namespace ArenaLegendsRPG.Tests.UiTests;

public class EncounterEventRenderTests
{
    private readonly Mock<IConsoleIO> _consoleIO = new();
    private readonly EncounterEventRender _sut;

    public EncounterEventRenderTests()
    {
        _sut = new EncounterEventRender(_consoleIO.Object);
    }

    [Fact]
    public void CanPresent_SupportedEvent_ReturnsTrue()
    {
        string eventType = "Structure";
        GameEvent gameEvent = eventType switch
        {
            "Structure" => new StructureEncounterStarted(),
            _ => throw new ArgumentOutOfRangeException(nameof(eventType))
        };

        var result = _sut.CanPresent(gameEvent);

        Assert.True(result);
    }

    [Fact]
    public void CanPresent_CharacterCreated_ReturnsFalse()
    {
        GameEvent gameEvent = new CharacterCreated("playerName");

        var result = _sut.CanPresent(gameEvent);

        Assert.False(result);
    }

    [Fact]
    public void Present_StructureEncounterStarted_WritesMessageAndReadsKey()
    {
        var gameEvent = new StructureEncounterStarted();

        _sut.Present(gameEvent);

        _consoleIO.Verify(c => c.WriteLine("Du hittade en byggnad!"), Times.Once);

        _consoleIO.Verify(c => c.ReadKey(), Times.Once);
    }

    [Fact]
    public void CanPresent_HealthRestored_ReturnsTrue()
    {
        var result = _sut.CanPresent(new HealthRestored("Hero", 20));

        Assert.True(result);
    }

    [Fact]
    public void Present_HealthRestored_WritesAmountAndReadsKey()
    {
        var gameEvent = new HealthRestored("Hero", 20);

        _sut.Present(gameEvent);

        _consoleIO.Verify(c => c.WriteLine("Du återhämtade dig och fick tillbaka 20HP!"), Times.Once);
        _consoleIO.Verify(c => c.ReadKey(), Times.Once);
    }
    [Fact]
    public void Present_MonsterEncounterStarted_WritesMonsterInfoAndReadsKey()
    {
        var gameEvent = new MonsterEncounterStarted("Goblin", 3, 55, 14, 9);

        _sut.Present(gameEvent);

        _consoleIO.Verify(c => c.WriteLine("Du möter en Goblin (level 3)!"), Times.Once);
        _consoleIO.Verify(c => c.WriteLine("HP: 55 | Attack: 14 | Magi: 9"), Times.Once);
        _consoleIO.Verify(c => c.ReadKey(), Times.Once);
    }
}