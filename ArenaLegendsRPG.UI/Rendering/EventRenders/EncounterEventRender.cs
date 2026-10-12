using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;

namespace ArenaLegendsRPG.UI.Rendering.EventRenders;

//FledFromEncounter
public class EncounterEventRender : IEventRender
{
    private readonly IConsoleIO _consoleIO;
    public EncounterEventRender(IConsoleIO consoleIO)
    {
        _consoleIO = consoleIO;
    }
    public bool CanPresent(GameEvent gameEvent)
    {
        return gameEvent is StructureEncounterStarted
            or ChestEncounterStarted
            or MonsterEncounterStarted
            or FledFromEncounter
            or FledFromEncounterFailed
            or DamageDealt;
    }
    public void Present(GameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case StructureEncounterStarted e:
                _consoleIO.WriteLine("Du hittade en byggnad!");
                _consoleIO.ReadKey();
                break;
            case ChestEncounterStarted e:
                _consoleIO.WriteLine("Du hittade en kista!");
                _consoleIO.ReadKey();
                break;
            case MonsterEncounterStarted e:
                _consoleIO.WriteLine("Du möte ett monster och han tillåter inte dig att gå vidare!");
                _consoleIO.ReadKey();
                break;
            case FledFromEncounter e:
                _consoleIO.WriteLine("Lyckades fly från monstret");
                _consoleIO.ReadKey();
                break;
            case FledFromEncounterFailed e:
                _consoleIO.WriteLine("Lyckades inte fly från monstret");
                _consoleIO.ReadKey();
                break;
        }
    }
}