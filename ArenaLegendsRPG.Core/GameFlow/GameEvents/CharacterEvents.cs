namespace ArenaLegendsRPG.Core.GameFlow.GameEvents;

public record CharacterCreated(string PlayerName) : GameEvent;
public record CharacterNameNotAllowed : GameEvent;