using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.UI.Rendering.Texts;

namespace ArenaLegendsRPG.UI.Rendering;

public static class MenuTexts
{
    public static string For(GameState state, MenuAction action)
    {
        return state switch
        {
            GameState.MainMenu =>
                MainMenuTexts.For(action),
            GameState.CharacterCreation =>
                CharacterCreationTexts.For(action),
            GameState.GameMenu =>
                GameMenuTexts.For(action),
            GameState.InEncounter =>
                EncounterMenuTexts.For(action),
            _ => "Okänt alternativ"
        };
    }
}