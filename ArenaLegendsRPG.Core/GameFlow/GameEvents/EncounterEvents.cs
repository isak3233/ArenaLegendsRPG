namespace ArenaLegendsRPG.Core.GameFlow.GameEvents;

public record MonsterEncounterStarted(string monsterName) : GameEvent;
public record ChestEncounterStarted : GameEvent;
public record StructureEncounterStarted : GameEvent;
public record DamageDealt(string AttackerName, string TargetName, int Amount) : GameEvent;

public record CombatRoundStats(string PlayerName, string AttackerName, int PlayerHp, int AttackerHp) : GameEvent;
public record CombatantDied(string Name) : GameEvent;
public record FledFromEncounter : GameEvent;
public record FledFromEncounterFailed : GameEvent;