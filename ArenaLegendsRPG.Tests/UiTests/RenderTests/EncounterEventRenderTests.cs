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


}