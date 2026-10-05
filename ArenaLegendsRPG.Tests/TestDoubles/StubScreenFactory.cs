using ArenaLegendsRPG.Core.GameFlow.Interfaces;

namespace ArenaLegendsRPG.Tests.TestDoubles;

internal class StubScreenFactory : IScreenFactory
{
    private readonly IGameScreen _mainMenu;

    public StubScreenFactory(IGameScreen mainMenu) => _mainMenu = mainMenu;


    public IGameScreen CreateMainMenu()
    {
        return _mainMenu;
    }

    public IGameScreen CreateCharacterCreation() => throw new NotImplementedException();
    public IGameScreen CreateEncounter() => throw new NotImplementedException();
    public IGameScreen CreateGameOver() => throw new NotImplementedException();
}