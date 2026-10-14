namespace ArenaLegendsRPG.Core.GameServices.Results;

public record SaveCharacterResult(bool Success, string? ErrorMessage = null);