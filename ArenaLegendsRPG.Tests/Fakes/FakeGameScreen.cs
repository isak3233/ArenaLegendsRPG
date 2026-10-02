using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.Tests.Fakes;

internal class FakeGameScreen : IGameScreen
{
    private readonly Dictionary<MenuAction, Func<ScreenResult>> _responses = new();

    public FakeGameScreen(GameState state, params MenuAction[] availableActions)
    {
        State = state;
        AvailableActions = availableActions;
    }

    public GameState State { get; }
    public IReadOnlyList<MenuAction> AvailableActions { get; }
    
    public List<MenuAction> ChosenActions { get; } = new();

    public IReadOnlyList<MenuAction> GetAvailableActions() => AvailableActions;

    public ScreenResult Choose(MenuAction action)
    {
        ChosenActions.Add(action);

        if (!_responses.TryGetValue(action, out var response))
        {
            throw new InvalidOperationException($"FakeGameScreen no answer for {action}.");
        }
        return response();
    }

    public FakeGameScreen WhenChosen(MenuAction action, IGameScreen next, params GameEvent[] events)
    {
        _responses[action] = () => ScreenResult.To(next, events);
        return this;
    }

    public FakeGameScreen WhenChosenThrows(MenuAction action, Exception exception)
    {
        _responses[action] = () => throw exception;
        return this;
    }
}