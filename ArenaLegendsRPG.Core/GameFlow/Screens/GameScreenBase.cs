using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.Core.GameFlow.Screens;

public abstract class GameScreenBase : IGameScreen
{
    public abstract GameState State { get; }
    public abstract IReadOnlyList<MenuAction> GetAvailableActions();

    public ScreenResult Choose(MenuAction action)
    {
        if (!GetAvailableActions().Contains(action))
        {
            throw new InvalidOperationException($"{action} is not available in this state {State}.");
        }
        return Handle(action);
    }

    protected abstract ScreenResult Handle(MenuAction action);
}