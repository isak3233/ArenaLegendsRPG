namespace ArenaLegendsRPG.Core.Game;

public interface IGame
{
    GameState State { get; }
    IReadOnlyList<MenuAction> GetAvailableActions();
    void Choose(MenuAction action);
}