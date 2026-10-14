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
    public async Task Choose_SwitchesToNextScreen()
    {
        var next = new StubGameScreen(GameState.InEncounter, MenuAction.Attack);
        var start = new StubGameScreen(GameState.MainMenu) { Next = next };
        var sut = CreateSut(start);

        await sut.ChooseAsync(MenuAction.StartNewGame);

        Assert.Equal(GameState.InEncounter, sut.State);
        Assert.Equal(new[] { MenuAction.Attack }, sut.GetAvailableActions());
    }

    [Fact]
    public async Task Choose_ReturnsEventsFromScreenResult()
    {
        var events = new GameEvent[] { new DummyEvent() };
        var start = new StubGameScreen(GameState.MainMenu) { Events = events };
        var sut = CreateSut(start);

        var result = await sut.ChooseAsync(MenuAction.Quit);

        Assert.Equal(events, result);
    }

    [Fact]
    public void Submit_OnTextInputScreen_SwitchesToNextScreenAndReturnsEvents()
    {
        var next = new StubGameScreen(GameState.GameMenu);
        var events = new GameEvent[] { new DummyEvent() };
        var start = new StubTextInputScreen(GameState.CharacterCreation, ScreenResult.To(next, events));
        var sut = CreateSut(start);

        var result = sut.Submit("Hero");

        Assert.Equal(GameState.GameMenu, sut.State);
        Assert.Equal(events, result);
    }

    [Fact]
    public void Submit_OnScreenWithoutTextInput_Throws()
    {
        var sut = CreateSut(new StubGameScreen(GameState.MainMenu));

        Assert.Throws<InvalidOperationException>(() => sut.Submit("Hero"));
    }

    [Fact]
    public void Select_OnSelectionScreen_StaysOnScreenAndReturnsEvents()
    {
        var events = new GameEvent[] { new DummyEvent() };
        var start = new StubSelectionScreen(GameState.Inventory, ScreenResult.To(new StubGameScreen(GameState.Inventory), events));
        var sut = CreateSut(start);

        var result = sut.Select(0);

        Assert.Equal(GameState.Inventory, sut.State);
        Assert.Equal(events, result);
    }

    [Fact]
    public void Select_OnScreenWithoutSelection_Throws()
    {
        var sut = CreateSut(new StubGameScreen(GameState.MainMenu));

        Assert.Throws<InvalidOperationException>(() => sut.Select(0));
    }


}