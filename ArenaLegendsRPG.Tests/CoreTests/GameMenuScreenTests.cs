using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.Screens;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests
{
    public class GameMenuScreenTests
    {
        [Fact]
        public void Choose_Explore_GoesToEncounter()
        {
            var encounter = new StubGameScreen(GameState.InEncounter);
            var factory = new StubScreenFactory(encounter: encounter);
            var screen = new GameMenuScreen(new GameSession(), factory);

            var result = screen.Choose(MenuAction.Explore);

            Assert.Equal(encounter, result.Next);

        }

        [Fact]
        public void Choose_Quit_GoesToGameOver()
        {
            var gameOver = new StubGameScreen(GameState.GameOver);
            var factory = new StubScreenFactory(gameOver: gameOver);
            var screen = new GameMenuScreen(new GameSession(), factory);

            var result = screen.Choose(MenuAction.Quit);

            Assert.Equal(gameOver, result.Next);

        }

    }
}
