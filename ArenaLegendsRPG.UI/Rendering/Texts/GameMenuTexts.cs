using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.UI.Rendering.Texts;

public class GameMenuTexts
{
    private static readonly Dictionary<MenuAction, string> Texts = new()
    {
        [MenuAction.Explore] = "Utforska världen",
        [MenuAction.OpenInventory] = "Hantera ditt inventory",
        [MenuAction.OpenBattleLog] = "Öppna din stridslogg",
        [MenuAction.SaveGame] = "Spara spel",
        [MenuAction.Quit] = "Avsluta",
    };

    public static string For(MenuAction action)
    {
        return Texts.TryGetValue(action, out var text) ? text : "Okänt alternativ";
    }
}