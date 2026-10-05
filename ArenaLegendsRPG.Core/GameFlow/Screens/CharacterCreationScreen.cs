using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.Core.GameFlow.Screens;

public class CharacterCreationScreen : GameScreenBase, ITextInputScreen
{
    private readonly GameSession _session;
    private readonly IScreenFactory _factory;

    public CharacterCreationScreen(GameSession session, IScreenFactory factory)
    {
        _session = session;
        _factory = factory;
    }

    public override GameState State => GameState.CharacterCreation;

    public override IReadOnlyList<MenuAction> GetAvailableActions()
    {
        return Array.Empty<MenuAction>();
    }


    protected override ScreenResult Handle(MenuAction action)
    {
        throw new NotImplementedException();
    }

    public ScreenResult Submit(string text)
    {
        throw new NotImplementedException();
    }
}