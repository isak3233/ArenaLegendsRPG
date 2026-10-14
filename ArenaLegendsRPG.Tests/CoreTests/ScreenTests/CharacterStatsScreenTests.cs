using ArenaLegendsRPG.Core.APIHandlers;
using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameFlow.Screens;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Tests.TestDoubles;
using Xunit;

namespace ArenaLegendsRPG.Tests.CoreTests.ScreenTests;

public class CharacterStatsScreenTests
{
    private readonly GameSession _session = new();
    private readonly CharacterStatsScreen _sut;
    private readonly StubScreenFactory _factory;

    public CharacterStatsScreenTests()
    {
        _session.SetPlayer(new Character("Hero", 100, 1, 0, 10, 0, 5, 2));

        _factory = new StubScreenFactory(gameMenu: new StubGameScreen(GameState.GameMenu));

        _sut = new CharacterStatsScreen(_session, _factory);
    }

    [Fact]
    public void State_ReturnsCharacterStats()
    {
        Assert.Equal(GameState.CharacterStats, _sut.State);
    }

    [Fact]
    public void GetAvailableActions_ReturnsExpectedActions()
    {
        var actions = _sut.GetAvailableActions();

        Assert.Equal(new[] { MenuAction.ShowStats, MenuAction.Back }, actions);
    }

    [Fact]
    public async Task Choose_ShowStats_ReturnsCharacterStatsEvent()
    {
        var result = await _sut.ChooseAsync(MenuAction.ShowStats);

        Assert.Equal(GameState.CharacterStats, result.Next.State);

        var characterStats = Assert.IsType<CharacterStats>(Assert.Single(result.Events));

        Assert.Equal(
            _session.RequirePlayer(),
            characterStats.Character);
    }

    [Fact]
    public async Task Choose_Back_ReturnsGameMenuScreen()
    {
        var result = await _sut.ChooseAsync(MenuAction.Back);

        Assert.Equal(GameState.GameMenu, result.Next.State);
    }
}