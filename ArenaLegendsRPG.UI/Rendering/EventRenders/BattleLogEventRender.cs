using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;

namespace ArenaLegendsRPG.UI.Rendering.EventRenders;

public class BattleLogEventRender : IEventRender
{
    private readonly IConsoleIO _consoleIO;
    public BattleLogEventRender(IConsoleIO consoleIO)
    {
        _consoleIO = consoleIO;
    }
    public bool CanPresent(GameEvent gameEvent)
    {
        return gameEvent is BattleLogInfo
            or BattleLogResult;
    }

    public void Present(GameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case BattleLogInfo e:
                _consoleIO.WriteLine("-----------------------------------------------");
                var winString = e.IsPlayerWinner ? "Du vann mot" : "Du förlorade mot";
                _consoleIO.WriteLine($"{winString} {e.MonsterName}\n" +
                                     $"Stats: {e.MonsterHealth}HP, {e.MonsterAttackDamage} Fysisk skada, {e.MonsterMagicDamage} Magisk skada, {e.MonsterAttackResist} Fysisk resistans, {e.MonsterMagicResist} Magisk resistans,\n" +
                                     $"Total skada mot {e.MonsterName} {e.DamageDoneByPlayer}, Total skada mot dig {e.DamageDoneByMonster}\n");
                break;
            case BattleLogResult e:
                _consoleIO.ReadKey();
                break;
        }
    }

}