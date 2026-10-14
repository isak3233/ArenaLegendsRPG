using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Tests.TestDoubles;
using ArenaLegendsRPG.UI;
using ArenaLegendsRPG.UI.Rendering.EventRenders;
using Moq;

namespace ArenaLegendsRPG.Tests.UiTests;

public class GameMenuEventRenderTests
{
    private readonly Mock<IConsoleIO> _consoleIO = new();
    private readonly GameMenuEventRender _sut;

    public GameMenuEventRenderTests()
    {
        _sut = new GameMenuEventRender(_consoleIO.Object);
    }

    private static GameEvent GetSaveSuccessEvent() => new SaveSuccess();
    private static GameEvent GetSaveFailedEvent() => new SaveFailed();


    [Fact]
    public void CanPresent_SaveSuccess_ReturnsTrue()
    {
        var result = _sut.CanPresent(GetSaveSuccessEvent());

        Assert.True(result);
    }

    [Fact]
    public void CanPresent_SaveFailed_ReturnsTrue()
    {
        var result = _sut.CanPresent(GetSaveFailedEvent());

        Assert.True(result);
    }

    [Fact]
    public void CanPresent_UnrelatedEvent_ReturnsFalse()
    {
        var result = _sut.CanPresent(new DummyEvent());

        Assert.False(result);
    }


    [Fact]
    public void Present_SaveSuccess_WritesSuccessMessage()
    {
        _sut.Present(GetSaveSuccessEvent());

        _consoleIO.Verify(c => c.WriteLine("Lyckades spara!"), Times.Once);
    }

    [Fact]
    public void Present_SaveSuccess_WaitsForKey()
    {
        _sut.Present(GetSaveSuccessEvent());

        _consoleIO.Verify(c => c.ReadKey(), Times.Once);
    }

    [Fact]
    public void Present_SaveFailed_WritesFailureMessage()
    {
        _sut.Present(GetSaveFailedEvent());

        _consoleIO.Verify(c => c.WriteLine("Lyckades inte spara :("), Times.Once);
    }

    [Fact]
    public void Present_SaveFailed_WaitsForKey()
    {
        _sut.Present(GetSaveFailedEvent());

        _consoleIO.Verify(c => c.ReadKey(), Times.Once);
    }

    [Fact]
    public void Present_UnrelatedEvent_WritesNothingAndDoesNotWait()
    {
        _sut.Present(new DummyEvent());

        _consoleIO.VerifyNoOtherCalls();
    }
}