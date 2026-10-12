using ArenaLegendsRPG.Core.GameFlow;

namespace ArenaLegendsRPG.UI.Rendering;

public interface IGameEventRender
{
    bool CanPresent(GameEvent gameEvent);
    void Present(GameEvent gameEvent);
}