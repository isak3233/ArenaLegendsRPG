using ArenaLegendsRPG.UI;
using ArenaLegendsRPG.UI.Rendering.Input;
using Moq;
using Xunit;

namespace ArenaLegendsRPG.Tests.UiTests;

public class InputHelperTests
{
    private readonly Mock<IConsoleIO> _consoleIO = new();
    private readonly InputHelper _sut;

    public InputHelperTests()
    {
        _sut = new InputHelper(_consoleIO.Object);
    }

    [Fact]
    public void GetString_WritesPromptAndReturnsInput()
    {
        _consoleIO.Setup(c => c.ReadLine()).Returns("test");
        
        var result = _sut.GetString("Skriv karaktärens namn: ");
        
        Assert.Equal("test", result);
        _consoleIO.Verify(c => c.Write("Skriv karaktärens namn: "), Times.Once);
        _consoleIO.Verify(c => c.ReadLine(), Times.Once);
    }

    [Fact]
    public void GetString_WhenInputIsNull_ReturnsEmptyString()
    {
        _consoleIO.Setup(c => c.ReadLine()).Returns((string?)null);
        
        var result = _sut.GetString("Ange namn: ");
        
        Assert.Equal(string.Empty, result);
    }
}