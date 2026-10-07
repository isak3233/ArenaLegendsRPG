using ArenaLegendsRPG.Core.GameFlow.Interfaces;

namespace ArenaLegendsRPG.Tests.TestDoubles;

internal class StubScreenFactory : IScreenFactory
{
    private readonly IGameScreen? _mainMenu;
    private readonly IGameScreen? _characterCreation;
    private readonly IGameScreen? _gameMenu;
    private readonly IGameScreen? _encounter;
    private readonly IGameScreen? _gameOver;

    public StubScreenFactory(
        IGameScreen? mainMenu = null,
        IGameScreen? characterCreation = null,
        IGameScreen? gameMenu = null,
        IGameScreen? encounter = null,
        IGameScreen? gameOver = null)
    {
        _mainMenu = mainMenu;
        _characterCreation = characterCreation;
        _gameMenu = gameMenu;
        _encounter = encounter;
        _gameOver = gameOver;
    }

    public IGameScreen CreateMainMenu() =>
        _mainMenu ?? throw new InvalidOperationException("MainMenu not configured for this test.");

    public IGameScreen CreateCharacterCreation() =>
        _characterCreation ?? throw new InvalidOperationException("CharacterCreation not configured for this test.");

    public IGameScreen CreateGameMenu() =>
        _gameMenu ?? throw new InvalidOperationException("Game menu not configured for this test");

    public IGameScreen CreateEncounter() =>
        _encounter ?? throw new InvalidOperationException("Encounter not configured for this test.");

    public IGameScreen CreateGameOver() =>
        _gameOver ?? throw new InvalidOperationException("GameOver not configured for this test.");
}