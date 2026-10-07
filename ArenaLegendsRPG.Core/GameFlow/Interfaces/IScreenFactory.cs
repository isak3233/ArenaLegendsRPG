namespace ArenaLegendsRPG.Core.GameFlow.Interfaces;

public interface IScreenFactory
{
    IGameScreen CreateMainMenu();
    IGameScreen CreateCharacterCreation();
    IGameScreen CreateGameMenu();
    IGameScreen CreateEncounter();
    IGameScreen CreateGameOver();
}