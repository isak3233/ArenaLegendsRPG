using ArenaLegendsRPG.Core.Encounters;
using ArenaLegendsRPG.Core.Monster;
using ArenaLegendsRPG.Core.RandomGen;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

namespace ArenaLegendsRPG.Core.GameServices;


public class EncounterService : IEncounterService
{
    private const int MonsterEncounterChancePercent = 85;
    private const int MonsterTypeCount = 3;

    private const int MinMonsterLevel = 1;
    private const int MaxLevelDifference = 1;

    private const int MinBaseDamageRoll = 6;
    private const int MaxBaseDamageRollExclusive = 15;

    private const int BaseMonsterHealth = 40;
    private const int BaseAttackResist = 2;
    private const int BaseMagicResist = 0;

    private const int HealthPerLevel = 10;
    private const int DamagePerLevel = 2;
    private const int ResistPerLevel = 1;

    private readonly IRandomProvider _random;
    private readonly ILootService _lootService;

    public EncounterService(IRandomProvider random, ILootService lootService)
    {
        _random = random;
        _lootService = lootService;
    }

    public IEncounter GenerateEncounter(int playerLevel)
    {
        var encounterType = _random.Next(0, 100);
        if (encounterType < MonsterEncounterChancePercent)
        {
            return new MonsterEncounter { Monster = CreateRandomMonster(playerLevel), Drop = _lootService.RollMonsterDrop() };
        }
        else
        {
            return new ChestEncounter { Loot = _lootService.GenerateLoot() };
        }
    }

    private IMonster CreateRandomMonster(int playerLevel)
    {
        var monsterType = _random.Next(0, MonsterTypeCount);
        var rolledAttackDamage = _random.Next(MinBaseDamageRoll, MaxBaseDamageRollExclusive);
        var rolledMagicDamage = _random.Next(MinBaseDamageRoll, MaxBaseDamageRollExclusive);
        var level = RollMonsterLevel(playerLevel);

        var levelsAboveFirst = level - MinMonsterLevel;
        var maxHealth = BaseMonsterHealth + HealthPerLevel * levelsAboveFirst;
        var attackDamage = rolledAttackDamage + DamagePerLevel * levelsAboveFirst;
        var magicDamage = rolledMagicDamage + DamagePerLevel * levelsAboveFirst;
        var attackResist = BaseAttackResist + ResistPerLevel * levelsAboveFirst;
        var magicResist = BaseMagicResist + ResistPerLevel * levelsAboveFirst;

        return monsterType switch
        {
            0 => new GoblinMonster(maxHealth, attackResist, magicResist, attackDamage, magicDamage, level),
            1 => new SkeletonMonster(maxHealth, attackResist, magicResist, attackDamage, magicDamage, level),
            _ => new SpiderMonster(maxHealth, attackResist, magicResist, attackDamage, magicDamage, level),
        };
    }

    private int RollMonsterLevel(int playerLevel)
    {
        var levelOffset = _random.Next(-MaxLevelDifference, MaxLevelDifference + 1);
        return Math.Max(MinMonsterLevel, playerLevel + levelOffset);
    }
}