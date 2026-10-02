using ArenaLegendsRPG.Core.Game.Interfaces;

namespace ArenaLegendsRPG.Core.Game;

public record ScreenResult(IGameScreen Next, IReadOnlyList<GameEvent> Events)
{
    public static ScreenResult To(IGameScreen next, params GameEvent[] events)
    {
        return new ScreenResult(next, events);
    }
}