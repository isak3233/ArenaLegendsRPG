using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.Tests.TestDoubles;

internal class StubTextInputScreen : StubGameScreen, ITextInputScreen
{
    private readonly ScreenResult _submitResult;

    public StubTextInputScreen(GameState state, ScreenResult submitResult) : base(state)
    {
        _submitResult = submitResult;
    }

    public List<string> SubmittedTexts { get; } = new();

    public ScreenResult Submit(string text)
    {
        SubmittedTexts.Add(text);
        return _submitResult;
    }
}