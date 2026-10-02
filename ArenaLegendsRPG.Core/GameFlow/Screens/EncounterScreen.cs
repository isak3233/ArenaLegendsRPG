using ArenaLegendsRPG.Core.Game.Interfaces;
using ArenaLegendsRPG.Core.Game.Menus;

namespace ArenaLegendsRPG.Core.Game.Screens;

public class EncounterScreen : GameScreenBase
{
    private readonly IScreenFactory _factory;

    public EncounterScreen(IScreenFactory factory)
    {
        _factory = factory;
    }

    public override GameState State => GameState.InEncounter;

    public override IReadOnlyList<MenuAction> GetAvailableActions()
    {
        return new[] { MenuAction.Attack, MenuAction.Flee };
    }

    protected override ScreenResult Handle(MenuAction action)
    {
        throw new NotImplementedException();
    }
}