using ArenaLegendsRPG.Core.APIHandlers;
using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.Fighting;
using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;
using ArenaLegendsRPG.Core.GameServices.Results;
using ArenaLegendsRPG.Core.Monster;
using ArenaLegendsRPG.Core.RandomGen;

namespace ArenaLegendsRPG.Core.GameServices;

public class CombatService : ICombatService
{
    private const int FleeSuccessThreshold = 50;

    private readonly IRandomProvider _random;
    private readonly IBattleLogService _battleLogService;
    private BattleLog _battleLog = new();

    public CombatService(
        IRandomProvider random,
        IBattleLogService battleLogService)
    {
        _random = random;
        _battleLogService = battleLogService;
    }

    public CombatRoundResult ProcessPlayerAttack(
        Character player,
        IMonster monster)
    {
        return ProcessPlayerAttack(player, monster, DamageType.Physical);
    }

    public CombatRoundResult ProcessPlayerAttack(
        Character player,
        IMonster monster,
        DamageType damageType)
    {
        var events = new List<GameEvent>();

        var playerDamageAmount = damageType switch
        {
            DamageType.Physical => player.AttackDamage,
            DamageType.Magic => player.MagicDamage,
            _ => throw new ArgumentOutOfRangeException(nameof(damageType))
        };

        var damageToMonster = monster.TakeDamage(
            new Damage(playerDamageAmount, damageType));

        events.Add(new DamageDealt(
            player.Name,
            monster.Name,
            damageToMonster,
            damageType));

        _battleLog.DamageDoneByPlayer += damageToMonster;

        if (monster.IsDead)
        {
            events.Add(new CombatantDied(monster.Name));
            _battleLog.IsPlayerWinner = true;
            _battleLog.BattleFinished = DateTime.Now;
            _battleLogService.SaveBattleLog(player.SavedPlayerId, _battleLog);

            return new CombatRoundResult(
                damageToMonster,
                0,
                true,
                false,
                events);
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

        var damageToPlayer = player.TakeDamage(
            new Damage(monsterDamageAmount, monsterDamageType));

        events.Add(new DamageDealt(
            monster.Name,
            player.Name,
            damageToPlayer,
            monsterDamageType));

        _battleLog.DamageDoneByMonster += damageToPlayer;

        var playerDied = player.Health <= 0;

        if (playerDied)
        {
            _battleLog.IsPlayerWinner = false;
            _battleLogService.SaveBattleLog(player.SavedPlayerId, _battleLog);
            events.Add(new CombatantDied(player.Name));
        }

        events.Add(new CombatRoundStats(
            player.Name,
            monster.Name,
            player.Health,
            monster.Health));

        return new CombatRoundResult(
            damageToMonster,
            damageToPlayer,
            false,
            playerDied,
            events);
    }

    public void AddMonsterInfoToBattleLog(IMonster monster)
    {
        _battleLog = new BattleLog
        {
            MonsterName = monster.Name,
            MonsterHealth = monster.Health,
            MonsterAttackResist = monster.AttackResist,
            MonsterMagicResist = monster.MagicResist,
            MonsterAttackDamage = monster.AttackDamage,
            MonsterMagicDamage = monster.MagicDamage,
        };
    }

    public FleeResult AttemptFlee()
    {
        var roll = _random.Next(0, 100);
        var succeeded = roll < FleeSuccessThreshold;

        List<GameEvent> events = succeeded
            ? new List<GameEvent> { new FledFromEncounter() }
            : new List<GameEvent> { new FledFromEncounterFailed() };

        return new FleeResult(succeeded, events);
    }
}