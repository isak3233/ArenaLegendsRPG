using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.Core.GameFlow.Screens;

public class GameOverScreen : IGameScreen
{
    public GameState State => GameState.GameOver;

    public IReadOnlyList<MenuAction> GetAvailableActions()
    {
        return new List<MenuAction>();
    }


    public ScreenResult Choose(MenuAction action)
    {
        throw new InvalidOperationException();
    }

}