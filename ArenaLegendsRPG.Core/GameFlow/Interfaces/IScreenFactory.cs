namespace ArenaLegendsRPG.Core.GameFlow.Interfaces;

public interface IScreenFactory
{
    IGameScreen CreateMainMenu();
    IGameScreen CreateEncounter();
    IGameScreen CreateGameOver();
}