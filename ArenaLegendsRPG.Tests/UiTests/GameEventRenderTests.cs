using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.UI;
using ArenaLegendsRPG.UI.Rendering;
using ArenaLegendsRPG.Tests.TestDoubles;
using Moq;
using Xunit;

public class GameEventRenderTests
{
    [Fact]
    public void Present_WhenPresenterCanPresent_CallsPresenter()
    {
        var gameEvent = new Mock<GameEvent>().Object;
        var presenter = new Mock<IEventRender>();
        var console = new FakeConsoleIO();

        presenter
            .Setup(p => p.CanPresent(gameEvent))
            .Returns(true);

        var sut = new GameEventRender(new[] { presenter.Object }, console);

        sut.Present(gameEvent);

        presenter.Verify(p => p.Present(gameEvent), Times.Once);
    }

    [Fact]
    public void Present_WhenNoPresenterCanPresent_WritesErrorMessage()
    {
        var gameEvent = new Mock<GameEvent>().Object;
        var presenter = new Mock<IEventRender>();
        var console = new FakeConsoleIO();

        presenter
            .Setup(p => p.CanPresent(gameEvent))
            .Returns(false);

        var sut = new GameEventRender(new[] { presenter.Object }, console);

        sut.Present(gameEvent);

        Assert.Contains($"Event saknar presenter: {gameEvent.GetType().Name}", console.Output);

        presenter.Verify(p => p.Present(It.IsAny<GameEvent>()), Times.Never);
    }

    [Fact]
    public void Present_WhenMultiplePresentersCanPresent_UsesFirstPresenter()
    {
        var gameEvent = new Mock<GameEvent>().Object;
        var firstPresenter = new Mock<IEventRender>();
        var secondPresenter = new Mock<IEventRender>();
        var console = new FakeConsoleIO();

        firstPresenter
            .Setup(p => p.CanPresent(gameEvent))
            .Returns(true);

        secondPresenter
            .Setup(p => p.CanPresent(gameEvent))
            .Returns(true);

        var sut = new GameEventRender(
            new[]
            {
                firstPresenter.Object,
                secondPresenter.Object
            },
            console);

        sut.Present(gameEvent);

        firstPresenter.Verify(p => p.Present(gameEvent), Times.Once);

        secondPresenter.Verify(p => p.Present(It.IsAny<GameEvent>()), Times.Never);
    }

    [Fact]
    public void PresentAll_PresentsEveryEvent()
    {
        var firstEvent = new Mock<GameEvent>().Object;
        var secondEvent = new Mock<GameEvent>().Object;
        var presenter = new Mock<IEventRender>();
        var console = new FakeConsoleIO();

        presenter
            .Setup(p => p.CanPresent(It.IsAny<GameEvent>()))
            .Returns(true);

        var sut = new GameEventRender(new[] { presenter.Object }, console);

        sut.PresentAll(new[] { firstEvent, secondEvent });

        presenter.Verify(p => p.Present(firstEvent), Times.Once);
        presenter.Verify(p => p.Present(secondEvent), Times.Once);
    }
}