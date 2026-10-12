using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.Tests.TestDoubles;

internal class StubSelectionScreen : StubGameScreen, ISelectionScreen
{
    private readonly ScreenResult _selectResult;

    public StubSelectionScreen(GameState state, ScreenResult selectResult) : base(state)
    {
        _selectResult = selectResult;
    }

    public ScreenResult Select(int index)
    {
        return _selectResult;
    }
}