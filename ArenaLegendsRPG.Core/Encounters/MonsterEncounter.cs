using ArenaLegendsRPG.Core.Monster;
using ArenaLegendsRPG.Core.Items.Interfaces;
namespace ArenaLegendsRPG.Core.Encounters;

public class MonsterEncounter : IEncounter
{
    public required IMonster Monster { get; init; }
    public IItem? Drop { get; set; }
}