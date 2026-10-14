using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;

namespace ArenaLegendsRPG.UI.Rendering.EventRenders;

public class CharacterStatsEventRender : IEventRender
{
    private readonly IConsoleIO _consoleIO;
    public CharacterStatsEventRender(IConsoleIO consoleIO)
    {
        _consoleIO = consoleIO;
    }
    public bool CanPresent(GameEvent gameEvent)
    {
        return gameEvent is CharacterStats;
    }

    public void Present(GameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case CharacterStats e:
                _consoleIO.WriteLine($"{e.Character.MaxHealth} Max Hp" +
                                     $"\n{e.Character.Health} HP" +
                                     $"\n{e.Character.AttackDamage} Fysisk skada" +
                                     $"\n{e.Character.MagicDamage} Magisk skada" +
                                     $"\n{e.Character.AttackResist} Fysisk Resistans" +
                                     $"\n{e.Character.MagicResist} Magisk Resistans" +
                                     $"\nLevel {e.Character.Level}" +
                                     $"\n{e.Character.MagicResist} Xp");
                _consoleIO.ReadKey();
                break;
        }
    }
}