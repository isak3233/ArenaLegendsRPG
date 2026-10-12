using ArenaLegendsRPG.Core.GameFlow;

namespace ArenaLegendsRPG.UI.Rendering;

public interface IEventRender
{
    bool CanPresent(GameEvent gameEvent);
    void Present(GameEvent gameEvent);
}