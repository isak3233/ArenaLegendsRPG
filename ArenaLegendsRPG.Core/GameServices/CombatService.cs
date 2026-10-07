using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.Monster;
using ArenaLegendsRPG.Core.Fighting;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.RandomGen;
using ArenaLegendsRPG.Core.GameFlow;

namespace ArenaLegendsRPG.Core.GameServices;

public interface ICombatService
{
    CombatRoundResult ProcessPlayerAttack(Character player, IMonster monster);
    FleeResult AttemptFlee();
}

public record CombatRoundResult(
    int DamageDealtToMonster,
    int DamageDealtToPlayer,
    bool MonsterDied,
    bool PlayerDied,
    IReadOnlyList<GameEvent> Events);

public record FleeResult(
    bool Succeeded,
    IReadOnlyList<GameEvent> Events);

public class CombatService : ICombatService
{
    private const int FleeSuccessThreshold = 50; //50% chance to flee.
    private readonly IRandomProvider _random;

    public CombatService(IRandomProvider random)
    {
        _random = random;
    }
    public CombatRoundResult ProcessPlayerAttack(Character player, IMonster monster)
    {
        var events = new List<GameEvent>();

        var damageToMonster = monster.TakeDamage(new Damage(player.AttackDamage, DamageType.Physical));
        events.Add(new DamageDealt(player.Name, "Monster", damageToMonster));

        if (monster.IsDead)
        {
            events.Add(new CombatantDied("Monster"));
            return new CombatRoundResult(damageToMonster, 0, true, false, events);

        }
        var (monsterDamageAmount, monsterDamageType) = monster.AttackDamage <= monster.MagicDamage
            ? (monster.AttackDamage, DamageType.Physical)
            : (monster.MagicDamage, DamageType.Magic);

        var damageToPlayer = player.TakeDamage(new Damage(monster.AttackDamage, DamageType.Physical));
        events.Add(new DamageDealt("Monster", player.Name, damageToPlayer));

        var playerDied = player.Health <= 0;
        if (playerDied)
        {
            events.Add(new CombatantDied(player.Name));
        }

        return new CombatRoundResult(damageToMonster, damageToPlayer, false, playerDied, events);
    }
    public FleeResult AttemptFlee()
    {
        var roll = _random.Next(0, 100);
        var succeeded = roll < FleeSuccessThreshold;

        var events = succeeded
            ? new List<GameEvent> { new FledFromEncounter() }
            : new List<GameEvent>();
        return new FleeResult(succeeded, events);
    }
}