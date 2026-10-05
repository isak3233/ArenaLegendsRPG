using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Core.GameFlow.Screens;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests;

public class CharacterCreationScreenTests
{
    [Fact]
    public void Submit_SetsPlayerOnSession()
    {
        var session = new GameSession();
        var factory = new StubScreenFactory(gameMenu: new StubGameScreen(GameState.GameMenu));
        var screen = new CharacterCreationScreen(session, factory);

        screen.Submit("Hero");

        Assert.Equal("Hero", session.RequirePlayer().Name);
    }

    [Fact]
    public void Submit_ReturnsCharacterCreatedEvent()
    {
        var session = new GameSession();
        var factory = new StubScreenFactory(gameMenu: new StubGameScreen(GameState.GameMenu));
        var screen = new CharacterCreationScreen(session, factory);

        var result = screen.Submit("Hero");

        Assert.Contains(result.Events, e => e is CharacterCreated created && created.PlayerName == "Hero");
    }

    [Fact]
    public void Submit_ReturnsGameMenuAsNextScreen()
    {
        var session = new GameSession();
        var gameMenu = new StubGameScreen(GameState.GameMenu);
        var factory = new StubScreenFactory(gameMenu: gameMenu);
        var screen = new CharacterCreationScreen(session, factory);

        var result = screen.Submit("Hero");

        Assert.Equal(gameMenu, result.Next);
    }
}