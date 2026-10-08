using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Screens;

using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

namespace ArenaLegendsRPG.Core.GameFlow;

public class ScreenFactory : IScreenFactory
{
    private readonly IGameSession _session;
    private readonly ICharacterCreationService _characterCreationService;
    private readonly IEncounterService _encounterService;
    private readonly ICombatService _combatService;

    public ScreenFactory(IGameSession session, ICharacterCreationService characterCreationService, IEncounterService encounterService, ICombatService combatService)
    {
        _session = session;
        _characterCreationService = characterCreationService;
        _encounterService = encounterService;
        _combatService = combatService;
    }

    public IGameScreen CreateMainMenu() => new MainMenuScreen(this);
    public IGameScreen CreateCharacterCreation() => new CharacterCreationScreen(_session, this, _characterCreationService);
    public IGameScreen CreateGameMenu() => new GameMenuScreen(this);
    public IGameScreen CreateEncounter() => new EncounterScreen(_session, this, _encounterService, _combatService);
    public IGameScreen CreateGameOver() => new GameOverScreen();
}