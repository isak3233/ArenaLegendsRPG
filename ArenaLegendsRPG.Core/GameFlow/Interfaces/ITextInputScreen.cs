namespace ArenaLegendsRPG.Core.Game.Interfaces;

public interface ITextInputScreen : IGameScreen
{
    ScreenResult Submit(string text);
}