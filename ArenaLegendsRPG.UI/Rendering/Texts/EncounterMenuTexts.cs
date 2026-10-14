using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.UI.Rendering.Texts;

public class EncounterMenuTexts
{
    private static readonly Dictionary<MenuAction, string> Texts = new()
    {
        [MenuAction.Attack] = "Attackera",
        [MenuAction.Flee] = "Försök fly",
        [MenuAction.OpenChest] = "Öppna kistan",
        [MenuAction.LeaveEncounter] = "Lämna",
        [MenuAction.LeaveEncounterScreen] = "Lämna",
        [MenuAction.BeginExplore] = "Börja utforska",
        [MenuAction.ContinueExploring] = "Fortsätt utforska"
    };

    public static string For(MenuAction action)
    {
        return Texts.TryGetValue(action, out var text) ? text : "Okänt alternativ";
    }
}