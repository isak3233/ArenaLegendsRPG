using ArenaLegendsRPG.Core.Monster;

namespace ArenaLegendsRPG.Core.Encounter;

public class MonsterEncounter : IEncounter
{
    public IMonster Monster { get; set; }
}