using ArenaLegendsRPG.Core.GameFlow;

namespace ArenaLegendsRPG.UI.Rendering.Input;

public interface IGameEventInputRouter
{
    InputResult? Handle(GameEvent gameEvent);
}