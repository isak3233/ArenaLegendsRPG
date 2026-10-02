namespace ArenaLegendsRPG.Core.Game.GameEvents;

public record EncounterStarted(string MonsterName) : GameEvent;
public record DamageDealt(string AttackerName, string TargetName, int Amount) : GameEvent;
public record CombatantDied(string Name) : GameEvent;
public record FledFromEncounter : GameEvent;