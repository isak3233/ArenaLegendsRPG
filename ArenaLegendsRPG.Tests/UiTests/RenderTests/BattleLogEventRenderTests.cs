using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.UI;
using ArenaLegendsRPG.UI.Rendering.EventRenders;

namespace ArenaLegendsRPG.Tests.UiTests;

using ArenaLegendsRPG.Core.GameFlow;
using Moq;
using Xunit;

public class BattleLogEventRenderTests
{
    private readonly Mock<IConsoleIO> _consoleIOMock;
    private readonly BattleLogEventRender _render;

    public BattleLogEventRenderTests()
    {
        _consoleIOMock = new Mock<IConsoleIO>();
        _render = new BattleLogEventRender(_consoleIOMock.Object);
    }

    [Fact]
    public void CanPresent_ShouldReturnTrue_WhenEventIsBattleLogInfo()
    {
        var gameEvent = new BattleLogInfo(
            "Dragon", 100, 20, 15, 30, 25,
            80, 40, true, DateTime.Now);

        var result = _render.CanPresent(gameEvent);

        Assert.True(result);
    }

    [Fact]
    public void CanPresent_ShouldReturnTrue_WhenEventIsBattleLogResult()
    {
        var gameEvent = new BattleLogResult(1);

        var result = _render.CanPresent(gameEvent);

        Assert.True(result);
    }

    [Fact]
    public void CanPresent_ShouldReturnFalse_WhenEventIsNull()
    {
        var result = _render.CanPresent(null!);

        Assert.False(result);
    }

    [Fact]
    public void Present_ShouldWriteBattleLogInfo_WhenPlayerWins()
    {
        var gameEvent = new BattleLogInfo(
            "Dragon", 100, 20, 15, 30, 25,
            80, 40, true,
            new DateTime(2026, 10, 14));

        _render.Present(gameEvent);

        _consoleIOMock.Verify(
            console => console.WriteLine(
                "-----------------------------------------------"),
            Times.Once);

        _consoleIOMock.Verify(
            console => console.WriteLine(
                "Du vann mot Dragon\n" +
                "Stats: 100HP, 30 Fysisk skada, 25 Magisk skada, " +
                "20 Fysisk resistans, 15 Magisk resistans,\n" +
                "Total skada mot Dragon 80, Total skada mot dig 40\n"),
            Times.Once);
    }

    [Fact]
    public void Present_ShouldWriteBattleLogInfo_WhenPlayerLoses()
    {
        var gameEvent = new BattleLogInfo(
            "Goblin", 50, 10, 5, 15, 10,
            30, 60, false,
            new DateTime(2026, 10, 14));

        _render.Present(gameEvent);

        _consoleIOMock.Verify(
            console => console.WriteLine(
                "Du förlorade mot Goblin\n" +
                "Stats: 50HP, 15 Fysisk skada, 10 Magisk skada, " +
                "10 Fysisk resistans, 5 Magisk resistans,\n" +
                "Total skada mot Goblin 30, Total skada mot dig 60\n"),
            Times.Once);
    }

    [Fact]
    public void Present_ShouldCallReadKey_WhenEventIsBattleLogResult()
    {

        var gameEvent = new BattleLogResult(1);

        _render.Present(gameEvent);


        _consoleIOMock.Verify(
            console => console.ReadKey(),
            Times.Once);
    }
}
