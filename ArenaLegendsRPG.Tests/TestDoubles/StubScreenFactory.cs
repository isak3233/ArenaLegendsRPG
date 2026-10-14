using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

namespace ArenaLegendsRPG.Tests.TestDoubles;

internal class StubScreenFactory : IScreenFactory
{
    private readonly IGameScreen? _mainMenu;
    private readonly IGameScreen? _characterCreation;
    private readonly IGameScreen? _gameMenu;
    private readonly IGameScreen? _encounter;
    private readonly IGameScreen? _gameOver;
    private readonly IGameScreen? _inventory;
    private readonly IGameScreen? _battleLog;

    public StubScreenFactory(
        IGameScreen? mainMenu = null,
        IGameScreen? characterCreation = null,
        IGameScreen? gameMenu = null,
        IGameScreen? encounter = null,
        IGameScreen? gameOver = null,
        IGameScreen? inventory = null,
        IGameScreen? battleLog = null)
    {
        _mainMenu = mainMenu;
        _characterCreation = characterCreation;
        _gameMenu = gameMenu;
        _encounter = encounter;
        _gameOver = gameOver;
        _inventory = inventory;
        _battleLog = battleLog;
    }

    public IGameScreen CreateMainMenu() =>
        _mainMenu ?? throw new InvalidOperationException("MainMenu not configured for this test.");

    public IGameScreen CreateCharacterCreation() =>
        _characterCreation ?? throw new InvalidOperationException("CharacterCreation not configured for this test.");

    public IGameScreen CreateGameMenu() =>
        _gameMenu ?? throw new InvalidOperationException("Game menu not configured for this test");

    public IGameScreen CreateInventory() =>
        _inventory ?? throw new InvalidOperationException("Inventory not configured for this test.");

    public IGameScreen CreateEncounter() =>
        _encounter ?? throw new InvalidOperationException("Encounter not configured for this test.");

    public IGameScreen CreateGameOver() =>
        _gameOver ?? throw new InvalidOperationException("GameOver not configured for this test.");

    public IGameScreen CreateBattleLog() =>
         _battleLog ?? throw new InvalidOperationException("BattleLog not configured for this test.");
}