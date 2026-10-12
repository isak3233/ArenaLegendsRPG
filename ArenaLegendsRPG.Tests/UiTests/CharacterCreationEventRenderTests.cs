using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.UI;
using ArenaLegendsRPG.UI.Rendering.EventRenders;
using Moq;
using Xunit;

namespace ArenaLegendsRPG.Tests.UiTests;

public class CharacterCreationEventRenderTests
{
    private readonly Mock<IConsoleIO> _consoleIO = new();
    private readonly CharacterCreationEventRender _sut;

    public CharacterCreationEventRenderTests()
    {
        _sut = new CharacterCreationEventRender(_consoleIO.Object);
    }

    [Fact]
    public void CanPresent_CharacterNameNotAllowed_ReturnsTrue()
    {
        var gameEvent = new CharacterNameNotAllowed();

        var result = _sut.CanPresent(gameEvent);

        Assert.True(result);
    }

    [Fact]
    public void CanPresent_CharacterCreated_ReturnsTrue()
    {
        var gameEvent = new CharacterCreated("testName");

        var result = _sut.CanPresent(gameEvent);

        Assert.True(result);
    }

    [Fact]
    public void CanPresent_StructureEncounterStarted_ReturnsFalse()
    {
        var gameEvent = new StructureEncounterStarted();

        var result = _sut.CanPresent(gameEvent);

        Assert.False(result);
    }

    [Fact]
    public void Present_CharacterNameNotAllowed_WritesMessageAndReadsKey()
    {
        var gameEvent = new CharacterNameNotAllowed();

        _sut.Present(gameEvent);

        _consoleIO.Verify(c => c.WriteLine("Namnet måste vara giltigt. Får inte vara tomt eller ha mer än 20 tecken."), Times.Once);

        _consoleIO.Verify(c => c.ReadKey(), Times.Once);
    }

    [Fact]
    public void Present_CharacterCreated_WritesMessageAndReadsKey()
    {
        var gameEvent = new CharacterCreated("test");

        _sut.Present(gameEvent);

        _consoleIO.Verify(c => c.WriteLine("Karaktären skapades!"), Times.Once);

        _consoleIO.Verify(c => c.ReadKey(), Times.Once);
    }
}