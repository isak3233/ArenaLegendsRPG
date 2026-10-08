using ArenaLegendsRPG.Core.Encounter;
using ArenaLegendsRPG.Core.Monster;
using ArenaLegendsRPG.Core.RandomGen;
using System.ComponentModel.DataAnnotations;

namespace ArenaLegendsRPG.Core.GameServices;

public interface IEncounterService
{
    IEncounter GenerateEncounter();
}

public class EncounterService : IEncounterService
{
    private readonly IRandomProvider _random;

    public EncounterService(IRandomProvider random)
    {
        _random = random;
    }

    public IEncounter GenerateEncounter()
    {
        var monsterType = _random.Next(0, 3);
        return monsterType switch
        {
            0 => new MonsterEncounter { Monster = CreateRandomMonster() },
            1 => new ChestEncounter(),
            _ => new StructureEncounter(),
        };
    }

    private IMonster CreateRandomMonster()
    {
        var monsterType = _random.Next(0, 3);
        var attackDamage = _random.Next(0, 10);
        var magicDamage = _random.Next(0, 10);

        return monsterType switch
        {
            0 => new GoblinMonster(attackDamage: attackDamage, magicDamage: magicDamage),
            1 => new SkeletonMonster(attackDamage: attackDamage, magicDamage: magicDamage),
            _ => new SpiderMonster(attackDamage: attackDamage, magicDamage: magicDamage),
        };
    }
}