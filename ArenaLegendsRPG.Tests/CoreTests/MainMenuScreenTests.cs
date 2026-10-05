using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Core.GameFlow.Screens;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests;

public class MainMenuScreenTests
{
    [Fact]
    public void Choose_StartNewGame_GoesToCharacterCreation()
    {
        var characterCreation = new StubGameScreen(GameState.CharacterCreation);
        var factory = new StubScreenFactory(characterCreation: characterCreation);
        var screen = new MainMenuScreen(factory);

        var result = screen.Choose(MenuAction.StartNewGame);

        Assert.Equal(characterCreation, result.Next);
    }


    [Fact]
    public void Choose_Quit_GoesToGameOver()
    {
        var gameOver = new StubGameScreen(GameState.GameOver);
        var factory = new StubScreenFactory(gameOver: gameOver);
        var screen = new MainMenuScreen(factory);

        var result = screen.Choose(MenuAction.Quit);

        Assert.Equal(gameOver, result.Next);
    }

}
