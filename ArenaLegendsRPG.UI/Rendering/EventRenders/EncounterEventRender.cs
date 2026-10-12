using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;

namespace ArenaLegendsRPG.UI.Rendering.EventRenders;

//FledFromEncounter
public class EncounterEventRender : IGameEventRender
{
    public bool CanPresent(GameEvent gameEvent)
    {
        return gameEvent is StructureEncounterStarted
            or ChestEncounterStarted
            or MonsterEncounterStarted
            or FledFromEncounter
            or FledFromEncounterFailed;
    }
    public void Present(GameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case StructureEncounterStarted e:
                Console.WriteLine("Du hittade en byggnad!");
                Console.ReadKey();
                break;
            case ChestEncounterStarted e:
                Console.WriteLine("Du hittade en kista!");
                Console.ReadKey();
                break;
            case MonsterEncounterStarted e:
                Console.WriteLine("Du möte ett monster och han tillåter inte dig att gå vidare!");
                Console.ReadKey();
                break;
            case FledFromEncounter e:
                Console.WriteLine("Lyckades fly från monstret");
                Console.ReadKey();
                break;
            case FledFromEncounterFailed e:
                Console.WriteLine("Lyckades inte fly från monstret");
                Console.ReadKey();
                break;

        }
    }
}