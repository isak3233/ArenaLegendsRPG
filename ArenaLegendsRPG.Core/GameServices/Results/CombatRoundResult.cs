using ArenaLegendsRPG.Core.GameFlow;

namespace ArenaLegendsRPG.Core.GameServices.Results;

public record CombatRoundResult(
    int DamageDealtToMonster,
    int DamageDealtToPlayer,
    bool MonsterDied,
    bool PlayerDied,
    IReadOnlyList<GameEvent> Events);