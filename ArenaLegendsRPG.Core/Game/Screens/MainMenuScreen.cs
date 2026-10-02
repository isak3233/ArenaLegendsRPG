namespace ArenaLegendsRPG.Core.Game;

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


    protected override IGameScreen Handle(MenuAction action)
    {
        switch (action)
        {
            case MenuAction.StartNewGame:
                return _factory.CreateEncounter();
                break;
            case MenuAction.Quit:
                return _factory.CreateGameOver();
                break;
            default:
                throw new InvalidOperationException();
                break;
        }
    }
}