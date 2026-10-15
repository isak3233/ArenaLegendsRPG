using ArenaLegendsRPG.Core.Fighting;
using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;

namespace ArenaLegendsRPG.UI.Rendering.EventRenders;

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
            or DamageDealt
            or CombatRoundStats
            or CombatantDied
            or ItemFound
            or XpGained
            or LeveledUp
            or HealthRestored;
    }

    public void Present(GameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case StructureEncounterStarted:
                _consoleIO.WriteLine("Du hittade en byggnad!");
                _consoleIO.ReadKey();
                break;

            case ChestEncounterStarted:
                _consoleIO.WriteLine("Du hittade en kista!");
                _consoleIO.ReadKey();
                break;

            case MonsterEncounterStarted e:
                _consoleIO.WriteLine(
                    $"Du möter en {e.Name} (level {e.Level})!");

                _consoleIO.WriteLine(
                    $"HP: {e.Health} | Attack: {e.AttackDamage} | Magi: {e.MagicDamage}");

                _consoleIO.WriteLine(
                    $"Fysisk resistans: {e.AttackResist} | Magisk resistans: {e.MagicResist}");

                _consoleIO.ReadKey();
                break;

            case FledFromEncounter:
                _consoleIO.WriteLine("Lyckades fly från monstret");
                _consoleIO.ReadKey();
                break;

            case FledFromEncounterFailed:
                _consoleIO.WriteLine("Lyckades inte fly från monstret");
                _consoleIO.ReadKey();
                break;

            case DamageDealt e:
                var damageType = e.Type == DamageType.Physical
                    ? "fysisk"
                    : "magisk";

                _consoleIO.WriteLine(
                    $"{e.AttackerName} gjorde {e.Amount} {damageType} skada mot {e.TargetName}");
                break;

            case CombatRoundStats e:
                _consoleIO.WriteLine(
                    $"{e.PlayerName}: {e.PlayerHp}HP \n{e.AttackerName}: {e.AttackerHp}HP");
                _consoleIO.ReadKey();
                break;

            case CombatantDied e:
                _consoleIO.WriteLine($"{e.Name} förlorade striden.");
                break;

            case ItemFound e:
                _consoleIO.WriteLine(
                    $"Du fick {e.ItemName}. Föremålet finns nu i ditt inventory!");
                _consoleIO.ReadKey();
                break;

            case XpGained e:
                _consoleIO.WriteLine($"Du fick {e.Amount}xp!");
                _consoleIO.ReadKey();
                break;

            case LeveledUp e:
                _consoleIO.WriteLine(
                    $"Du levlade upp! Du är nu level {e.NewLevel}");
                _consoleIO.ReadKey();
                break;

            case HealthRestored e:
                _consoleIO.WriteLine(
                    $"Du återhämtade dig och fick tillbaka {e.Amount}HP!");
                _consoleIO.ReadKey();
                break;
        }
    }
}