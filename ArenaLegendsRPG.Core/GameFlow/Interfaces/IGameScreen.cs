using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.Core.GameFlow.Interfaces;

public interface IGameScreen
{
    GameState State { get; }
    IReadOnlyList<MenuAction> GetAvailableActions();
    ScreenResult Choose(MenuAction action);
}