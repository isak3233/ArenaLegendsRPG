namespace ArenaLegendsRPG.Core.GameFlow.Interfaces;

public interface ITextInputScreen : IGameScreen
{
    ScreenResult Submit(string text);
}