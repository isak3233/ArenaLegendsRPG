using ArenaLegendsRPG.Core.Game.Interfaces;
using ArenaLegendsRPG.Core.Game.Screens;

namespace ArenaLegendsRPG.Core.Game;

public class ScreenFactory : IScreenFactory
{
    public IGameScreen CreateMainMenu() => new MainMenuScreen(this);
    public IGameScreen CreateEncounter() => new EncounterScreen(this);
    public IGameScreen CreateGameOver() => new GameOverScreen();
}