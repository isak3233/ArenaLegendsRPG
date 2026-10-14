namespace ArenaLegendsRPG.Core.GameFlow.GameEvents;

public record MonsterEncounterStarted(string Name, int Level, int Health, int AttackDamage, int MagicDamage) : GameEvent;
public record ChestEncounterStarted : GameEvent;
public record StructureEncounterStarted : GameEvent;
public record DamageDealt(string AttackerName, string TargetName, int Amount) : GameEvent;

public record CombatRoundStats(string PlayerName, string AttackerName, int PlayerHp, int AttackerHp) : GameEvent;
public record CombatantDied(string Name) : GameEvent;
public record FledFromEncounter : GameEvent;
public record FledFromEncounterFailed : GameEvent;
public record HealthRestored(string PlayerName, int Amount) : GameEvent;