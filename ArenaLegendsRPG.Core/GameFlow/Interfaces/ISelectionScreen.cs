namespace ArenaLegendsRPG.Core.GameFlow.Interfaces;

public interface ISelectionScreen : IGameScreen
{
    ScreenResult Select(int index);
}