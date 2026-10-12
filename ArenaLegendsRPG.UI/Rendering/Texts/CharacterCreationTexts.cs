using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.UI.Rendering.Texts;

public class CharacterCreationTexts
{
    private static readonly Dictionary<MenuAction, string> Texts = new()
    {
        [MenuAction.CreateNewCharacter] = "Skapa en ny karaktär",
        [MenuAction.LoadCharacter]  = "Ladda karaktär",
    };

    public static string For(MenuAction action)
    {
        return Texts.TryGetValue(action, out var text) ? text : "Okänt alternativ";
    }
}