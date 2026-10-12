using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.UI.Rendering;

namespace ArenaLegendsRPG.Tests.UiTests;

using Xunit;

public class MenuTextsTests
{
    [Fact]
    public void For_UnknownState_ReturnsUnknownOption()
    {

        var state = (GameState)999;
        var action = default(MenuAction);
        
        var result = MenuTexts.For(state, action);


        Assert.Equal("Okänt alternativ", result);
    }

    [Theory]
    [InlineData(GameState.MainMenu, MenuAction.StartNewGame)]
    [InlineData(GameState.CharacterCreation, MenuAction.CreateNewCharacter)]
    [InlineData(GameState.GameMenu, MenuAction.Explore)]
    [InlineData(GameState.InEncounter, MenuAction.BeginExplore)]
    public void For_KnownState_ReturnsText(GameState state, MenuAction action)
    {
        
        var result = MenuTexts.For(state, action);
        
        Assert.NotNull(result);
        Assert.NotEqual("Okänt alternativ", result);
    }
}