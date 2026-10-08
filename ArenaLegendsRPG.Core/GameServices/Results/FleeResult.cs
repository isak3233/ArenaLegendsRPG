using ArenaLegendsRPG.Core.GameFlow;

namespace ArenaLegendsRPG.Core.GameServices.Results;

public record FleeResult(bool Succeeded, IReadOnlyList<GameEvent> Events);