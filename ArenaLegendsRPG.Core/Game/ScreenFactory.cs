namespace ArenaLegendsRPG.Core.Game.Screens;

public class ScreenFactory : IScreenFactory
{
    public IGameScreen CreateMainMenu() => new MainMenuScreen(this);
    public IGameScreen CreateEncounter() => new EncounterScreen(this);
    public IGameScreen CreateGameOver() => new GameOverScreen();
}