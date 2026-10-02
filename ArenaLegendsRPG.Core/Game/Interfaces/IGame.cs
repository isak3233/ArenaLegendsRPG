using ArenaLegendsRPG.Core.Game.Menus;

namespace ArenaLegendsRPG.Core.Game.Interfaces;

public interface IGame
{
    GameState State { get; }
    IReadOnlyList<MenuAction> GetAvailableActions();
    IReadOnlyList<GameEvent> Choose(MenuAction action);
}