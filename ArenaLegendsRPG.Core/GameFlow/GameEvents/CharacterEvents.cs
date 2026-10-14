using ArenaLegendsRPG.Core.Characters;

namespace ArenaLegendsRPG.Core.GameFlow.GameEvents;

public record CreateNewCharacter : GameEvent;
public record CharacterCreated(string PlayerName) : GameEvent;
public record CharacterNameNotAllowed : GameEvent;
public record SaveSuccess : GameEvent;
public record SaveFailed : GameEvent;
public record LoadSuccess : GameEvent;
public record LoadFailed : GameEvent;
public record LoadedCharacter(Character Character, int SavedId) : GameEvent;
public record NoSavedCharacters : GameEvent;
public record CharacterLoaded : GameEvent;
public record InvalidCharacterSelection : GameEvent;