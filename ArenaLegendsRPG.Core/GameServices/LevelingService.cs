using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;

namespace ArenaLegendsRPG.Core.GameServices;

public interface ILevelingService
{
    int XpRequiredForNextLevel(int level);
    IReadOnlyList<GameEvent> GainXp(Character character, int amount);
}

public class LevelingService : ILevelingService
{
    private const int XpPerLevel = 100;
    private const int MaxHealthPerLevel = 10;
    private const int AttackDamagePerLevel = 2;
    private const int MagicDamagePerLevel = 1;
    private const int AttackResistPerLevel = 1;
    private const int MagicResistPerLevel = 1;

    public int XpRequiredForNextLevel(int level)
    {
        return level * XpPerLevel;
    }

    public IReadOnlyList<GameEvent> GainXp(Character character, int amount)
    {
        var events = new List<GameEvent> { new XpGained(character.Name, amount) };

        character.AddXp(amount);

        while (character.Xp >= XpRequiredForNextLevel(character.Level))
        {
            character.LevelUp(XpRequiredForNextLevel(character.Level));
            character.IncreaseMaxHealth(MaxHealthPerLevel);
            character.IncreaseBaseStats(AttackDamagePerLevel, MagicDamagePerLevel, AttackResistPerLevel, MagicResistPerLevel);
            character.Heal(character.MaxHealth);

            events.Add(new LeveledUp(character.Name, character.Level));
        }

        return events;
    }
}