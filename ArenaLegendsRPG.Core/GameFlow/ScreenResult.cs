using ArenaLegendsRPG.Core.GameFlow.Interfaces;

namespace ArenaLegendsRPG.Core.GameFlow;

public record ScreenResult(IGameScreen Next, IReadOnlyList<GameEvent> Events)
{
    public static ScreenResult To(IGameScreen next, params GameEvent[] events)
    {
        return new ScreenResult(next, events);
    }
}