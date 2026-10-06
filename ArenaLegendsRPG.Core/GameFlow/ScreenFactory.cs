using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Screens;

namespace ArenaLegendsRPG.Core.GameFlow;

public class ScreenFactory : IScreenFactory
{
    private readonly GameSession _session;
    
    public ScreenFactory(GameSession session)
    {
        _session = session;
    }
    public IGameScreen CreateMainMenu() => new MainMenuScreen(this);
    public IGameScreen CreateCharacterCreation() => new CharacterCreationScreen(_session, this);
    public IGameScreen CreateGameMenu() => new GameMenuScreen(_session, this);
    public IGameScreen CreateEncounter() => new EncounterScreen(this);
    public IGameScreen CreateGameOver() => new GameOverScreen();
}