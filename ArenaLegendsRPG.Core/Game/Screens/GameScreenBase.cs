using ArenaLegendsRPG.Core.Game.Interfaces;
using ArenaLegendsRPG.Core.Game.Menus;

namespace ArenaLegendsRPG.Core.Game;

public abstract class GameScreenBase : IGameScreen
{
    public abstract GameState State { get; }
    public abstract IReadOnlyList<MenuAction> GetAvailableActions();

    public IGameScreen Choose(MenuAction action)
    {
        if (!GetAvailableActions().Contains(action))
        {
            throw new InvalidOperationException($"{action} is not available in this state {State}.");
        }
        return Handle(action);
    }

    protected abstract IGameScreen Handle(MenuAction action);
}