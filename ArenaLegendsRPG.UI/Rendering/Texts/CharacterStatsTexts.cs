using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.UI.Rendering.Texts;

public class CharacterStatsTexts
{
    private static readonly Dictionary<MenuAction, string> Texts = new()
    {
        [MenuAction.ShowStats] = "Visa din karaktärs stats",
        [MenuAction.Back] = "Lämna",
    };

    public static string For(MenuAction action)
    {
        return Texts.TryGetValue(action, out var text) ? text : "Okänt alternativ";
    }
}