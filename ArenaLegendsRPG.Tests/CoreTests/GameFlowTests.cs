using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests;

public class GameFlowTests
{
    private static GameFlow CreateSut(IGameScreen startScreen)
    {
        return new GameFlow(new StubScreenFactory(startScreen));
    }


    [Fact]
    public void NewGame_StartsOnMainMenuScreenFromFactory()
    {
        var factory = new StubScreenFactory(new StubGameScreen(GameState.MainMenu));

        var sut = new GameFlow(factory);

        Assert.Equal(GameState.MainMenu, sut.State);
    }

    [Fact]
    public void GetAvailableActions_ReturnsActionsFromCurrentScreen()
    {
        var start = new StubGameScreen(GameState.MainMenu, MenuAction.StartNewGame, MenuAction.Quit);
        var sut = CreateSut(start);

        Assert.Equal(new[] { MenuAction.StartNewGame, MenuAction.Quit }, sut.GetAvailableActions());
    }
    [Fact]
    public void Choose_SwitchesToNextScreen()
    {
        var next = new StubGameScreen(GameState.InEncounter, MenuAction.Attack);
        var start = new StubGameScreen(GameState.MainMenu) { Next = next };
        var sut = CreateSut(start);

        sut.Choose(MenuAction.StartNewGame);

        Assert.Equal(GameState.InEncounter, sut.State);
        Assert.Equal(new[] { MenuAction.Attack }, sut.GetAvailableActions());
    }

    [Fact]
    public void Choose_ReturnsEventsFromScreenResult()
    {
        var events = new GameEvent[] { new DummyEvent() };
        var start = new StubGameScreen(GameState.MainMenu) { Events = events };
        var sut = CreateSut(start);

        var result = sut.Choose(MenuAction.Quit);

        Assert.Equal(events, result);
    }




}