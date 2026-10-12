using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.Core.GameFlow;

public class GameFlow : IGameFlow
{
    private IGameScreen _current;

    public GameFlow(IScreenFactory factory)
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
        return MoveTo(_current.Choose(action));
    }
    // Just add input? in choose method and use input records in a later version
    public IReadOnlyList<GameEvent> Submit(string text)
    {
        if (_current is not ITextInputScreen textScreen)
        {
            throw new InvalidOperationException($"The screen {State} does not accept text input.");
        }

        return MoveTo(textScreen.Submit(text));
    }


    private IReadOnlyList<GameEvent> MoveTo(ScreenResult result)
    {
        _current = result.Next;
        return result.Events;
    }
}