namespace ArenaLegendsRPG.Core.Game;

public interface IScreenFactory
{
    IGameScreen CreateMainMenu() ;
    IGameScreen CreateEncounter();
    IGameScreen CreateGameOver();
}