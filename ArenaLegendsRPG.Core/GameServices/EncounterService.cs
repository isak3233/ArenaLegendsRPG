using ArenaLegendsRPG.Core.Encounters;
using ArenaLegendsRPG.Core.Monster;
using ArenaLegendsRPG.Core.RandomGen;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

namespace ArenaLegendsRPG.Core.GameServices;


public class EncounterService : IEncounterService
{
    private readonly IRandomProvider _random;
    private readonly ILootService _lootService;

    public EncounterService(IRandomProvider random, ILootService lootService)
    {
        _random = random;
        _lootService = lootService;
    }

    public IEncounter GenerateEncounter()
    {
        var encounterType = _random.Next(0, 100);
        if (encounterType < 85)
        {
            return new MonsterEncounter { Monster = CreateRandomMonster(), Drop = _lootService.RollMonsterDrop() };
        }
        else
        {
            return new ChestEncounter { Loot = _lootService.GenerateLoot() };
        }
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