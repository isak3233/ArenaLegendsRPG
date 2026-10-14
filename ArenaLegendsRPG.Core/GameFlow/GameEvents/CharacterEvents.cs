namespace ArenaLegendsRPG.Core.GameFlow.GameEvents;

public record CreateNewCharacter : GameEvent;
public record CharacterCreated(string PlayerName) : GameEvent;
public record CharacterNameNotAllowed : GameEvent;