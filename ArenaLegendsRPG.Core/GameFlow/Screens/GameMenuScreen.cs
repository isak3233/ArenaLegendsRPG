using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.Core.GameFlow.Screens;

public class GameMenuScreen : IGameScreen
{
    private readonly IScreenFactory _factory;

    public GameMenuScreen(IScreenFactory factory)
    {
        _factory = factory;
    }
    public GameState State => GameState.GameMenu;

    public IReadOnlyList<MenuAction> GetAvailableActions()
    {
        return new[] { MenuAction.Explore, MenuAction.OpenInventory, MenuAction.SaveGame, MenuAction.Quit };

    }

    public ScreenResult Choose(MenuAction action)
    {
        return action switch
        {
            MenuAction.Explore => ScreenResult.To(_factory.CreateEncounter()),
            MenuAction.OpenInventory => ScreenResult.To(_factory.CreateInventory()),
            MenuAction.SaveGame => throw new NotImplementedException("Save not built yet."),
            MenuAction.Quit => ScreenResult.To(_factory.CreateGameOver()),
            _ => throw new InvalidOperationException($"Unhandled action {action} in {State}.")
        };
    }
}