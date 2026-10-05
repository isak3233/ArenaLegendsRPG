using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.Core.GameFlow.Interfaces;

public interface IGame
{
    GameState State { get; }
    IReadOnlyList<MenuAction> GetAvailableActions();
    IReadOnlyList<GameEvent> Choose(MenuAction action);
}