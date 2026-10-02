using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.Core.GameFlow;

public class Game : IGame
{
    private IGameScreen _current;
    

    public Game(IScreenFactory factory)
    {
        _current = factory.CreateMainMenu();
    }

    public GameState State => _current.State;

    public IReadOnlyList<MenuAction> GetAvailableActions()
    {
        return _current.GetAvailableActions();
    }


    public IReadOnlyList<GameEvent> Choose(MenuAction action)
    {
        var result = _current.Choose(action);
        _current = result.Next;
        return result.Events;
    }
        
}