using ArenaLegendsRPG.Core.Game.Interfaces;

namespace ArenaLegendsRPG.Tests.Fakes;

internal class FakeScreenFactory : IScreenFactory
{
    private readonly IGameScreen _mainMenu;

    public FakeScreenFactory(IGameScreen mainMenu) => _mainMenu = mainMenu;

    public int CreateMainMenuCalls { get; private set; }

    public IGameScreen CreateMainMenu()
    {
        CreateMainMenuCalls++;
        return _mainMenu;
    }
    
    public IGameScreen CreateCharacterCreation() => throw new NotImplementedException();
    public IGameScreen CreateEncounter() => throw new NotImplementedException();
    public IGameScreen CreateGameOver() => throw new NotImplementedException();
}