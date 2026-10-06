using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Core.GameFlow.Screens;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests
{
    public class CharacterCreationScreenFixture
    {
        public GameSession Session { get; } = new();
        public StubGameScreen GameMenu { get; } = new(GameState.GameMenu);
        public CharacterCreationScreen Screen { get; }

        public CharacterCreationScreenFixture()
        {
            var factory = new StubScreenFactory(gameMenu: GameMenu);
            Screen = new CharacterCreationScreen(Session, factory, new CharacterCreationService());
        }
    }
}
