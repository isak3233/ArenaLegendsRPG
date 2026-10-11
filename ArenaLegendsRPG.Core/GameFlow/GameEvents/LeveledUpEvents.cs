namespace ArenaLegendsRPG.Core.GameFlow.GameEvents;

public record LeveledUp(string PlayerName, int NewLevel) : GameEvent;