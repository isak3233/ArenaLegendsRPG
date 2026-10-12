using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.UI.Rendering.Texts;

public class MainMenuTexts
{
    private static readonly Dictionary<MenuAction, string> Texts = new()
    {
        [MenuAction.StartNewGame] = "Starta ett nytt Arena Legends Spel!",
        [MenuAction.Quit]  = "Avsluta",
    };

    public static string For(MenuAction action)
    {
        return Texts.TryGetValue(action, out var text) ? text : "Okänt alternativ";
    }
}