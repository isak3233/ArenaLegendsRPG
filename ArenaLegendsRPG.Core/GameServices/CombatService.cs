using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.Monster;
using ArenaLegendsRPG.Core.Fighting;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.RandomGen;
using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;
using ArenaLegendsRPG.Core.GameServices.Results;

namespace ArenaLegendsRPG.Core.GameServices;

public class CombatService : ICombatService
{
    private const int FleeSuccessThreshold = 50;
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

        int monsterDamageAmount;
        DamageType monsterDamageType;
        if (monster.AttackDamage >= monster.MagicDamage)
        {
            monsterDamageAmount = monster.AttackDamage;
            monsterDamageType = DamageType.Physical;
        }
        else
        {
            monsterDamageAmount = monster.MagicDamage;
            monsterDamageType = DamageType.Magic;
        }

        var damageToPlayer = player.TakeDamage(new Damage(monsterDamageAmount, monsterDamageType));
        events.Add(new DamageDealt("Monster", player.Name, damageToPlayer));

        var playerDied = player.Health <= 0;
        if (playerDied)
        {
            events.Add(new CombatantDied(player.Name));
        }

        return new CombatRoundResult(damageToMonster, monsterDamageAmount, false, playerDied, events);
    }
    public FleeResult AttemptFlee()
    {
        var roll = _random.Next(0, 100);
        var succeeded = roll < FleeSuccessThreshold;
        List<GameEvent> events;
        if (succeeded)
        {
            events = new List<GameEvent> { new FledFromEncounter() };
        }
        else
        {
            events = new List<GameEvent>();
        }
        return new FleeResult(succeeded, events);
    }
}