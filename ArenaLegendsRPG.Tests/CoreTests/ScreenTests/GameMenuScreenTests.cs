using ArenaLegendsRPG.Core.APIHandlers;
using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.Screens;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests.ScreenTests;

public class GameMenuScreenTests
{
    private readonly GameSession _session = new();
    private readonly GameMenuScreen _sut;

    public GameMenuScreenTests()
    {
        _session.SetPlayer(new Character("Hero", 100, 1, 0, 10, 0, 5, 2));

        var factory = new StubScreenFactory(
            encounter: new StubGameScreen(GameState.InEncounter),
            gameOver: new StubGameScreen(GameState.GameOver),
            inventory: new StubGameScreen(GameState.Inventory));

        _sut = new GameMenuScreen(_session, factory, new StubCharacterCreationService());
    }

    [Theory]
    [InlineData(MenuAction.Explore, GameState.InEncounter)]
    [InlineData(MenuAction.OpenInventory, GameState.Inventory)]
    [InlineData(MenuAction.Quit, GameState.GameOver)]
    [InlineData(MenuAction.SaveGame, GameState.GameMenu)]
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