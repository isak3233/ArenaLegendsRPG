using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Tests.Fakes;

namespace ArenaLegendsRPG.Tests.CoreTests;

public class GameTests
{
    private static Game CreateSut(IGameScreen startScreen)
    {
        return new Game(new FakeScreenFactory(startScreen));
    }
        

    [Fact]
    public void NewGame_StartsOnMainMenuScreenFromFactory()
    {
        var factory = new FakeScreenFactory(new FakeGameScreen(GameState.MainMenu));

        var sut = new Game(factory);

        Assert.Equal(GameState.MainMenu, sut.State);
        Assert.Equal(1, factory.CreateMainMenuCalls);
    }

    [Fact]
    public void GetAvailableActions_ReturnsActionsFromCurrentScreen()
    {
        var start = new FakeGameScreen(GameState.MainMenu, MenuAction.StartNewGame, MenuAction.Quit);
        var sut = CreateSut(start);

        Assert.Equal(new[] { MenuAction.StartNewGame, MenuAction.Quit }, sut.GetAvailableActions());
    }
    [Fact]
    public void Choose_SwitchesToNextScreen()
    {
        var next = new FakeGameScreen(GameState.InEncounter, MenuAction.Attack);
        var start = new FakeGameScreen(GameState.MainMenu)
            .WhenChosen(MenuAction.StartNewGame, next);
        var sut = CreateSut(start);

        sut.Choose(MenuAction.StartNewGame);

        Assert.Equal(GameState.InEncounter, sut.State);
        Assert.Equal(new[] { MenuAction.Attack }, sut.GetAvailableActions());
    }

    [Fact]
    public void Choose_ReturnsEventsFromScreenResult()
    {
        var events = new GameEvent[] { new FakeEvent() };
        var start = new FakeGameScreen(GameState.MainMenu)
            .WhenChosen(MenuAction.Quit, new FakeGameScreen(GameState.GameOver), events);
        var sut = CreateSut(start);

        var result = sut.Choose(MenuAction.Quit);

        Assert.Equal(events, result);
    }




}