using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Tests.TestDoubles;
using ArenaLegendsRPG.UI;
using ArenaLegendsRPG.UI.Rendering;
using ArenaLegendsRPG.UI.Rendering.Input;
using Moq;
using Xunit;

namespace ArenaLegendsRPG.Tests.UiTests;

public class ConsoleGameLoopTests
{
    private readonly Mock<IGameFlow> _game = new();
    private readonly Mock<IGameEventRender> _eventRender = new();
    private readonly Mock<IGameEventInputRouter> _inputRouter = new();

    private ConsoleGameLoop CreateSut(FakeConsoleIO console)
    {
        return new ConsoleGameLoop(
            _game.Object,
            _eventRender.Object,
            _inputRouter.Object,
            console);
    }

    [Fact]
    public async Task Run_ValidMenuChoice_CallsChoose()
    {
        var console = new FakeConsoleIO();
        console.SetInput("1");

        var state = GameState.MainMenu;

        _game.SetupGet(g => g.State)
            .Returns(() => state);

        _game.Setup(g => g.GetAvailableActions())
            .Returns(new[] { default(MenuAction) });

        _game.Setup(g => g.ChooseAsync(It.IsAny<MenuAction>()))
            .Callback<MenuAction>(_ => state = GameState.GameOver)
            .ReturnsAsync(Array.Empty<GameEvent>());

        var sut = CreateSut(console);

        await sut.RunAsync();

        _game.Verify(g => g.ChooseAsync(It.IsAny<MenuAction>()), Times.Once);
    }

    [Fact]
    public async Task Run_InvalidMenuChoice_DoesNotCallChoose()
    {
        var console = new FakeConsoleIO();
        console.SetInput("abc");

        _game.SetupSequence(g => g.State)
            .Returns(GameState.MainMenu)
            .Returns(GameState.MainMenu)
            .Returns(GameState.GameOver);

        _game.Setup(g => g.GetAvailableActions())
            .Returns(new[] { default(MenuAction) });

        var sut = CreateSut(console);

        await sut.RunAsync();

        Assert.Contains("Ogiltigt val.", console.Output);

        _game.Verify(g => g.ChooseAsync(It.IsAny<MenuAction>()), Times.Never);
    }

    [Fact]
    public async Task Run_EventWithoutInput_PresentsEvent()
    {
        var console = new FakeConsoleIO();
        console.SetInput("1");

        var state = GameState.MainMenu;
        var gameEvent = new CreateNewCharacter();

        _game.SetupGet(g => g.State)
            .Returns(() => state);

        _game.Setup(g => g.GetAvailableActions())
            .Returns(new[] { default(MenuAction) });

        _game.Setup(g => g.ChooseAsync(It.IsAny<MenuAction>()))
            .Callback<MenuAction>(_ => state = GameState.GameOver)
            .ReturnsAsync(new[] { gameEvent });

        _inputRouter.Setup(r => r.Handle(gameEvent))
            .Returns((InputResult?)null);

        var sut = CreateSut(console);

        await sut.RunAsync();

        _eventRender.Verify(
            r => r.PresentAll(
                It.Is<IEnumerable<GameEvent>>(
                    events => events.Count() == 1 &&
                              events.First() == gameEvent)),
            Times.Once);
    }
}