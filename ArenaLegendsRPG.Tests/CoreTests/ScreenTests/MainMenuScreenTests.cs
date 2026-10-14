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
    public async Task Choose_GoesToExpectedScreen(MenuAction action, GameState expectedState)
    {
        var result = await _sut.ChooseAsync(action);

        Assert.Equal(expectedState, result.Next.State);
    }
    [Fact]
    public async Task Choose_ActionNotInMenu_Throws()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(() => _sut.ChooseAsync(MenuAction.Attack));
    }
}