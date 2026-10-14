using ArenaLegendsRPG.Core.Items.Interfaces;

namespace ArenaLegendsRPG.Core.Encounters;

public class ChestEncounter : IEncounter
{
    public required IItem Loot { get; init; }
}