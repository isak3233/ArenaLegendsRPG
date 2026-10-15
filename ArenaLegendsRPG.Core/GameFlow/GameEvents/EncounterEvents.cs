using ArenaLegendsRPG.Core.Fighting;

namespace ArenaLegendsRPG.Core.GameFlow.GameEvents;

public record MonsterEncounterStarted(
    string Name,
    int Level,
    int Health,
    int AttackDamage,
    int MagicDamage,
    int AttackResist = 0,
    int MagicResist = 0) : GameEvent;

public record ChestEncounterStarted : GameEvent;

public record StructureEncounterStarted : GameEvent;

public record DamageDealt(
    string AttackerName,
    string TargetName,
    int Amount,
    DamageType Type) : GameEvent;

public record CombatRoundStats(
    string PlayerName,
    string AttackerName,
    int PlayerHp,
    int AttackerHp) : GameEvent;

public record CombatantDied(string Name) : GameEvent;

public record FledFromEncounter : GameEvent;

public record FledFromEncounterFailed : GameEvent;

public record HealthRestored(string PlayerName, int Amount) : GameEvent;