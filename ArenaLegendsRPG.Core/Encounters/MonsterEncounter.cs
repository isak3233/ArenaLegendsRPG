using ArenaLegendsRPG.Core.Monster;

namespace ArenaLegendsRPG.Core.Encounters;

public class MonsterEncounter : IEncounter
{
    public required IMonster Monster { get; init; }
}