using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Screens;
using ArenaLegendsRPG.Core.GameServices;

namespace ArenaLegendsRPG.Core.GameServices;

public class ScreenFactory : IScreenFactory
{
    private readonly GameSession _session = new();
    private readonly ICharacterCreationService _characterCreationService = new CharacterCreationService();

    public IGameScreen CreateMainMenu() => new MainMenuScreen(this);
    public IGameScreen CreateCharacterCreation() => new CharacterCreationScreen(_session, this, _characterCreationService);
    public IGameScreen CreateGameMenu() => new GameMenuScreen(_session, this);
    public IGameScreen CreateEncounter() => new EncounterScreen(this);
    public IGameScreen CreateGameOver() => new GameOverScreen();
}