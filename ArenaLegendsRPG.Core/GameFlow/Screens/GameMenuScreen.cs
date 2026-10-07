using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.Core.GameFlow.Screens;

public class GameMenuScreen : GameScreenBase
{
    private readonly IGameSession _session;
    private readonly IScreenFactory _factory;

    public GameMenuScreen(IGameSession session, IScreenFactory factory)
    {
        _session = session;
        _factory = factory;
    }
    public override GameState State => GameState.GameMenu;

    public override IReadOnlyList<MenuAction> GetAvailableActions()
    {
        return new[] { MenuAction.Explore, MenuAction.OpenInventory, MenuAction.SaveGame, MenuAction.Quit };

    }

    protected override ScreenResult Handle(MenuAction action)
    {
        return action switch
        {
            MenuAction.Explore => ScreenResult.To(_factory.CreateEncounter()),
            MenuAction.OpenInventory => throw new NotImplementedException("Inventory screen not built yet."),
            MenuAction.SaveGame => throw new NotImplementedException("Save not built yet."),
            MenuAction.Quit => ScreenResult.To(_factory.CreateGameOver()),
            _ => throw new InvalidOperationException($"Unhandled action {action} in {State}.")
        };
    }
}






