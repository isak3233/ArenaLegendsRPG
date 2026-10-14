using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.UI.Rendering.Texts;

public class InventoryMenuTexts
{
    private static readonly Dictionary<MenuAction, string> Texts = new()
    {
        [MenuAction.OpenInventoryManagment] = "Öppna ditt inventory",
        [MenuAction.Back] = "Lämna",
    };

    public static string For(MenuAction action)
    {
        return Texts.TryGetValue(action, out var text) ? text : "Okänt alternativ";
    }
}