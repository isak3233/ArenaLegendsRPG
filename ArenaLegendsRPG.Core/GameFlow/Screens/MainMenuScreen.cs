using ArenaLegendsRPG.Core.Game.Interfaces;
using ArenaLegendsRPG.Core.Game.Menus;

namespace ArenaLegendsRPG.Core.Game.Screens;

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
        //switch (action)
        //{
        //    case MenuAction.StartNewGame:
        //        return ScreenResult.To(_factory.CreateEncounter());
        //    case MenuAction.Quit:
        //        return ScreenResult.To(_factory.CreateGameOver());
        //    default:
        //        throw new InvalidOperationException();
        //}
        throw new NotImplementedException();
    }
}