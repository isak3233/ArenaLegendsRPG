using ArenaLegendsRPG.Core.GameServices;

namespace ArenaLegendsRPG.Tests.CoreTests.ServiceTests;

using ArenaLegendsRPG.Core.Characters;

public class CharacterCreationServiceTest
{
    private readonly CharacterCreationService _sut = new();

    [Theory]
    [InlineData("Hero", true)]
    [InlineData("A", true)]                                  
    [InlineData("12345678901234567890", true)]               
    [InlineData("123456789012345678901", false)]            
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData("  Hero  ", true)]                           
    public void ValidateCharacterName_ReturnsExpected(string name, bool expected)
    {
        Assert.Equal(expected, _sut.ValidateCharacterName(name));
    }

    [Fact]
    public void ValidateCharacterName_Null_ReturnsFalse()
    {
        Assert.False(_sut.ValidateCharacterName(null!));
    }

    [Fact]
    public void CreateCharacter_ReturnsCharacter()
    {
        var result = _sut.CreateCharacter("Hero");

        Assert.Equal("Hero", result.Name);
    }
}