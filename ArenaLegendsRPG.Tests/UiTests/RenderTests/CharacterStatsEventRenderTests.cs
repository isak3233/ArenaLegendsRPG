using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.UI;
using ArenaLegendsRPG.UI.Rendering.EventRenders;
using Moq;
using Xunit;

namespace ArenaLegendsRPG.Tests.UiTests;

public class CharacterStatsEventRenderTests
{
    private readonly Mock<IConsoleIO> _consoleIO = new();
    private readonly CharacterStatsEventRender _sut;
    private Character GetCharacter() => new Character("test", 100, 1, 1, 10, 5, 3, 1);
    public CharacterStatsEventRenderTests()
    {
        _sut = new CharacterStatsEventRender(_consoleIO.Object);
    }

    [Fact]
    public void CanPresent_CharacterStats_ReturnsTrue()
    {
        GameEvent gameEvent = new CharacterStats(GetCharacter());

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
    public void Present_CharacterStats_WritesStatsAndReadsKey()
    {
        var character = GetCharacter();

        var gameEvent = new CharacterStats(character);

        var expectedMessage =
            "100 Max Hp" +
            "\n100 HP" +
            "\n10 Fysisk skada" +
            "\n5 Magisk skada" +
            "\n3 Fysisk Resistans" +
            "\n1 Magisk Resistans" +
            "\nLevel 1" +
            "\n1 Xp";

        _sut.Present(gameEvent);

        _consoleIO.Verify(
            c => c.WriteLine(expectedMessage),
            Times.Once);

        _consoleIO.Verify(
            c => c.ReadKey(),
            Times.Once);
    }
}
