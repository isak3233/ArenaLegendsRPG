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
        var monsterType = _random.Next(0, 3);
        return monsterType switch
        {
            0 => new MonsterEncounter { Monster = CreateRandomMonster(), Drop = _lootService.RollMonsterDrop() },
            1 => new ChestEncounter { Loot = _lootService.GenerateLoot() },
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