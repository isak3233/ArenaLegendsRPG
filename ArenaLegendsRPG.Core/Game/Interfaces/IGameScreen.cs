namespace ArenaLegendsRPG.Core.Game;

public interface IGameScreen
{
    GameState State { get; }
    IReadOnlyList<MenuAction> GetAvailableActions();
    IGameScreen Choose(MenuAction action);
}