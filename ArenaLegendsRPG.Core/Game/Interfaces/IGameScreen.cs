using ArenaLegendsRPG.Core.Game.Menus;

namespace ArenaLegendsRPG.Core.Game.Interfaces;

public interface IGameScreen
{
    GameState State { get; }
    IReadOnlyList<MenuAction> GetAvailableActions();
    ScreenResult Choose(MenuAction action);
}