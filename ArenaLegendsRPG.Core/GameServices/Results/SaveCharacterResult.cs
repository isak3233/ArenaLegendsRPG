namespace ArenaLegendsRPG.Core.GameServices.Results;

public record SaveCharacterResult(bool Success, int SavedId, string? ErrorMessage = null);