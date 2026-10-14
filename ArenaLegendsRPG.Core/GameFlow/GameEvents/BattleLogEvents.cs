namespace ArenaLegendsRPG.Core.GameFlow.GameEvents;

public record BattleLogInfo(
    string MonsterName,
    int MonsterHealth,
    int MonsterAttackResist,
    int MonsterMagicResist,
    int MonsterAttackDamage,
    int MonsterMagicDamage,
    int DamageDoneByPlayer,
    int DamageDoneByMonster,
    bool IsPlayerWinner,
    DateTime BattleFinished)
    : GameEvent;
public record BattleLogResult(int Count) : GameEvent;
