namespace ArenaLegendsRPG.Core.GameFlow.GameEvents;

public record ItemEquipped(string PlayerName, string ItemName) : GameEvent;
public record InvalidItemSelection(int Index) : GameEvent;