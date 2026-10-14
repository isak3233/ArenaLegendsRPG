using ArenaLegendsRPG.Core.APIHandlers;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

namespace ArenaLegendsRPG.Core.GameFlow.Screens;

public class GameMenuScreen : IGameScreen
{
    private readonly IScreenFactory _factory;
    private readonly ICharacterCreationService _characterCreationService;
    private readonly IGameSession _gameSession;

    public GameMenuScreen(IGameSession gameSession, IScreenFactory factory, ICharacterCreationService characterCreationService)
    {
        _gameSession = gameSession;
        _factory = factory;
        _characterCreationService = characterCreationService;

    }
    public GameState State => GameState.GameMenu;

    public IReadOnlyList<MenuAction> GetAvailableActions()
    {
        return new[] { MenuAction.Explore, MenuAction.OpenInventory, MenuAction.SaveGame, MenuAction.Quit };

    }

    public async Task<ScreenResult> ChooseAsync(MenuAction action)
    {
        return action switch
        {
            MenuAction.Explore => ScreenResult.To(_factory.CreateEncounter()),
            MenuAction.OpenInventory => ScreenResult.To(_factory.CreateInventory()),
            MenuAction.SaveGame => await HandleSaveGame(),
            MenuAction.Quit => ScreenResult.To(_factory.CreateGameOver()),
            _ => throw new InvalidOperationException($"Unhandled action {action} in {State}.")
        };
    }

    private async Task<ScreenResult> HandleSaveGame()
    {
        var character = _gameSession.RequirePlayer();
        try
        {
            await _characterCreationService.SaveAsync(character);
            return ScreenResult.To(this, new SaveSuccess());
        }
        catch (Exception e)
        {
            return ScreenResult.To(this, new SaveFailed());
        }
    }
}