namespace ArenaLegendsRPG.Core.Game.Interfaces;

public interface IScreenFactory
{
    IGameScreen CreateMainMenu() ;
    IGameScreen CreateEncounter();
    IGameScreen CreateGameOver();
}