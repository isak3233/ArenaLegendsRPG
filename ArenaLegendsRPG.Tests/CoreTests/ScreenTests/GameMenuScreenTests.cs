
using ArenaLegendsRPG.Core.GameFlow.Screens;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests.ScreenTests;

public class GameMenuScreenTests
{
    private readonly GameMenuScreen _sut;

    public GameMenuScreenTests()
    {
        var factory = new StubScreenFactory(encounter: new StubGameScreen(GameState.InEncounter), gameOver: new StubGameScreen(GameState.GameOver));

        _sut = new GameMenuScreen(factory);
    }

    [Theory]
    [InlineData(MenuAction.Explore, GameState.InEncounter)]
    [InlineData(MenuAction.Quit, GameState.GameOver)]
    public void Choose_GoesToExpectedScreen(MenuAction action, GameState expectedState)
    {
        var result = _sut.Choose(action);

        Assert.Equal(expectedState, result.Next.State);
    }
    [Fact]
    public void Choose_ActionNotInMenu_Throws()
    {
        Assert.Throws<InvalidOperationException>(() => _sut.Choose(MenuAction.Attack));
    }
}

