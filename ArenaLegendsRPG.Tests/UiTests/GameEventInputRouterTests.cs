using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.UiTests;

using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.UI.Rendering.Input;
using Moq;
using Xunit;

public class GameEventInputRouterTests
{
    [Fact]
    public void Handle_CreateNewCharacter_ReturnsInputResult()
    {
        var inputHelper = new Mock<IInputHelper>();

        inputHelper
            .Setup(x => x.GetString("Skriv karaktärens namn: "))
            .Returns("Isak&Jakob");

        var sut = new GameEventInputRouter(inputHelper.Object);
        var gameEvent = new CreateNewCharacter();

        var result = sut.Handle(gameEvent);

        Assert.NotNull(result);
        Assert.Equal(InputType.String, result.Type);
        Assert.Equal("Isak&Jakob", result.Value);

        inputHelper.Verify(x => x.GetString("Skriv karaktärens namn: "), Times.Once);
    }

    [Fact]
    public void Handle_UnknownEvent_ReturnsNull()
    {
        var inputHelper = new Mock<IInputHelper>();
        var gameEvent = new DummyEvent();

        var sut = new GameEventInputRouter(inputHelper.Object);

        var result = sut.Handle(gameEvent);

        Assert.Null(result);

        inputHelper.Verify(x => x.GetString(It.IsAny<string>()), Times.Never);
    }

}