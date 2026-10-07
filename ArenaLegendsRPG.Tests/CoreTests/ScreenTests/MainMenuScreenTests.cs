using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Core.GameFlow.Screens;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests.ScreenTests;

public class MainMenuScreenTests
{
    private readonly MainMenuScreen _sut;

    public MainMenuScreenTests()
    {
        var factory = new StubScreenFactory(characterCreation: new StubGameScreen(GameState.CharacterCreation), gameOver: new StubGameScreen(GameState.GameOver));

        _sut = new MainMenuScreen(factory);
    }

    [Theory]
    [InlineData(MenuAction.StartNewGame, GameState.CharacterCreation)]
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