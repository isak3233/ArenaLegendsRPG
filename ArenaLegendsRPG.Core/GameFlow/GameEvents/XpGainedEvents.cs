namespace ArenaLegendsRPG.Core.GameFlow.GameEvents;

public record XpGained(string PlayerName, int Amount) : GameEvent;