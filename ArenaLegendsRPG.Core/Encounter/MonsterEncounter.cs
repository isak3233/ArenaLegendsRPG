using ArenaLegendsRPG.Core.Monster;

namespace ArenaLegendsRPG.Core.Encounter;

public class MonsterEncounter : IEncounter
{
    public required IMonster Monster { get; init; }
}