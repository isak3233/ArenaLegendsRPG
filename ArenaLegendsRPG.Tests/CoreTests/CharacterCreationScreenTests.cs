using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Core.GameFlow.Screens;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests;

public class CharacterCreationScreenTests
{
    private readonly GameSession _session = new();
    private readonly StubGameScreen _gameMenu = new(GameState.GameMenu);
    private readonly CharacterCreationScreen _sut;

    public CharacterCreationScreenTests()
    {
        var factory = new StubScreenFactory(gameMenu: _gameMenu);
        _sut = new CharacterCreationScreen(_session, factory, new CharacterCreationService());
    }

    [Fact]
    public void Submit_SetsPlayerOnSession()
    {
        _sut.Submit("Hero");

        Assert.Equal("Hero", _session.RequirePlayer().Name);
    }

    [Fact]
    public void Submit_ReturnsCharacterCreatedEvent()
    {
        var result = _sut.Submit("Hero");

        Assert.Contains(result.Events, e => e is CharacterCreated created && created.PlayerName == "Hero");
    }

    [Fact]
    public void Submit_ReturnsGameMenuAsNextScreen()
    {
        var result = _sut.Submit("Hero");

        Assert.Equal(_gameMenu, result.Next);
    }
}