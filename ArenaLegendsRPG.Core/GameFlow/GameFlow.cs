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

    public async Task<IReadOnlyList<GameEvent>> ChooseAsync(MenuAction action)
    {
        return MoveTo(await _current.ChooseAsync(action));
    }

    public IReadOnlyList<GameEvent> Submit(string text)
    {
        if (_current is not ITextInputScreen textScreen)
        {
            throw new InvalidOperationException($"The screen {State} does not accept text input.");
        }

        return MoveTo(textScreen.Submit(text));
    }


    public IReadOnlyList<GameEvent> Select(int index)
    {
        if (_current is not ISelectionScreen selectionScreen)
        {
            throw new InvalidOperationException($"The screen {State} does not accept a selection.");
        }

        return MoveTo(selectionScreen.Select(index));
    }


    private IReadOnlyList<GameEvent> MoveTo(ScreenResult result)
    {
        _current = result.Next;
        return result.Events;
    }
}