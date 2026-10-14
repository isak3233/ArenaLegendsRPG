namespace ArenaLegendsRPG.Core.GameFlow.GameEvents;

public record ItemEquipped(string PlayerName, string ItemName, bool Equipped) : GameEvent;
public record WeaponEvent(string Name, string Description, int Attack, int Magic, bool IsEquipped, int Index) : GameEvent;
public record ProtectionEvent(string Name, string Description, int Attack, int Magic, bool IsEquipped, int Index) : GameEvent;
public record InvalidItemSelection(int Index) : GameEvent;
public record InventoryManagmentOpen : GameEvent;