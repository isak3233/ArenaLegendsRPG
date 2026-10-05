using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.Tests.TestDoubles;

internal class StubGameScreen : IGameScreen
{
    public StubGameScreen(GameState state, params MenuAction[] availableActions)
    {
        State = state;
        AvailableActions = availableActions;
    }

    public GameState State { get; }
    public IReadOnlyList<MenuAction> AvailableActions { get; }

    public IGameScreen? Next { get; init; }
    public IReadOnlyList<GameEvent> Events { get; init; } = Array.Empty<GameEvent>();


    public IReadOnlyList<MenuAction> GetAvailableActions() => AvailableActions;

    public ScreenResult Choose(MenuAction action)
    {
        return new ScreenResult(Next ?? this, Events);
    }
}