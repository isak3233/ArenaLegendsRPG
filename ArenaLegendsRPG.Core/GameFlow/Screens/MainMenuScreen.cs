using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.Core.GameFlow.Screens;

public class MainMenuScreen : GameScreenBase
{
    private readonly IScreenFactory _factory;

    public MainMenuScreen(IScreenFactory factory)
    {
        _factory = factory;
    }

    public override GameState State => GameState.MainMenu;

    public override IReadOnlyList<MenuAction> GetAvailableActions()
    {
        return new[] { MenuAction.StartNewGame, MenuAction.Quit };
    }


    protected override ScreenResult Handle(MenuAction action)
    {
        
        return action switch
        {
            MenuAction.StartNewGame => ScreenResult.To(_factory.CreateCharacterCreation()),
            MenuAction.Quit => ScreenResult.To(_factory.CreateGameOver()),
            _ => throw new InvalidOperationException($"Unhandled action {action} in {State}.")
        };
    }
}