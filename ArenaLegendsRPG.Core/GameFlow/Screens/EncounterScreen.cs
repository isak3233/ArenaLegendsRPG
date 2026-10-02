using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.Core.GameFlow.Screens;

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