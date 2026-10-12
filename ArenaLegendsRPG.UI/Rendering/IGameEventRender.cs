using ArenaLegendsRPG.Core.GameFlow;

namespace ArenaLegendsRPG.UI.Rendering;

public interface IGameEventRender
{
    void PresentAll(IEnumerable<GameEvent> events);
    void Present(GameEvent gameEvent);
}