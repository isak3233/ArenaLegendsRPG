using ArenaLegendsRPG.Core.APIHandlers;
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
    private readonly IRewardService _rewardService;
    private readonly IInventoryService _inventoryService;
    private readonly IBattleLogService _battleLogService;

    public ScreenFactory(
        IGameSession session,
        ICharacterCreationService characterCreationService,
        IEncounterService encounterService,
        ICombatService combatService,
        IRewardService rewardService,
        IInventoryService inventoryService,
        IBattleLogService battleLogService
        )
    {
        _session = session;
        _characterCreationService = characterCreationService;
        _encounterService = encounterService;
        _combatService = combatService;
        _rewardService = rewardService;
        _inventoryService = inventoryService;
        _battleLogService = battleLogService;
    }

    public IGameScreen CreateMainMenu() => new MainMenuScreen(this);
    public IGameScreen CreateCharacterCreation() => new CharacterCreationScreen(_session, this, _characterCreationService);
    public IGameScreen CreateGameMenu() => new GameMenuScreen(_session, this, _characterCreationService);
    public IGameScreen CreateInventory() => new InventoryScreen(_session, this, _inventoryService);
    public IGameScreen CreateBattleLog() => new BattleLogScreen(_session, this, _battleLogService);
    public IGameScreen CreateEncounter() => new EncounterScreen(_session, this, _encounterService, _combatService, _rewardService);
    public IGameScreen CreateCharacterStats() => new CharacterStatsScreen(_session, this);
    public IGameScreen CreateGameOver() => new GameOverScreen();
}