using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Tests.TestDoubles;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;
using ArenaLegendsRPG.Core.RandomGen;
using IBattleLogService = ArenaLegendsRPG.API.Features.BattleLog.IBattleLogService;

namespace ArenaLegendsRPG.Tests.CoreTests.ServiceTests;

public class CombatServiceTests
{
    private static Character CreateCharacter(int health = 100, int attackDamage = 10, int attackResist = 0)
    {
        return new Character("Hero", health, 1, 0, attackDamage, baseMagicDamage: 0, attackResist, baseMagicResist: 0);
    }

    private ICombatService GetCombatService(IRandomProvider? randomProvider = null)
    {
        if (randomProvider == null)
        {
            randomProvider = new StubRandomProvider();
        }
        return new CombatService(randomProvider, new StubBattleLogService());

    }

    [Fact]
    public void ProcessPlayerAttack_MonsterSurvives_BothDealDamage()
    {
        var player = CreateCharacter(attackDamage: 10);
        var monster = new TestMonster(maxHealth: 30, attackResist: 0, magicResist: 0, attackDamage: 5);
        var service = GetCombatService();

        var result = service.ProcessPlayerAttack(player, monster);
        Assert.Equal(10, result.DamageDealtToMonster);
        Assert.Equal(5, result.DamageDealtToPlayer);
        Assert.False(result.MonsterDied);
        Assert.False(result.PlayerDied);
    }

    [Fact]
    public void ProcessPlayerAttack_MonsterDies_NoCounterattack()
    {
        var player = CreateCharacter(attackDamage: 100);
        var monster = new TestMonster(maxHealth: 10, attackResist: 0, magicResist: 0, attackDamage: 999);
        var service = GetCombatService();

        var result = service.ProcessPlayerAttack(player, monster);

        Assert.True(result.MonsterDied);
        Assert.Equal(0, result.DamageDealtToPlayer);
        Assert.Equal(100, player.Health);
    }
    [Fact]
    public void ProcessPlayerAttack_MonsterHasHigherMagicDamage_DealsMagicDamageToPlayer()
    {
        var player = CreateCharacter(attackDamage: 10, attackResist: 0);
        var monster = new TestMonster(maxHealth: 30, attackResist: 0, magicResist: 0, attackDamage: 2, magicDamage: 8);
        var service = GetCombatService();

        var result = service.ProcessPlayerAttack(player, monster);

        Assert.Equal(8, result.DamageDealtToPlayer);
    }
    [Fact]
    public void AttemptFlee_LowRoll_Succeeds()
    {
        var random = new StubRandomProvider(10);
        var service = GetCombatService(random);

        var result = service.AttemptFlee();

        Assert.True(result.Succeeded);
    }

    [Fact]
    public void AttemptFlee_HighRoll_Fails()
    {
        var random = new StubRandomProvider(80);
        var service = GetCombatService(random);

        var result = service.AttemptFlee();

        Assert.False(result.Succeeded);
    }
    [Fact]
    public void ProcessPlayerAttack_PlayerHasResist_ReportsActualDamageAfterResist()
    {
        var player = CreateCharacter(attackResist: 6);
        var monster = new TestMonster(maxHealth: 100, attackResist: 0, magicResist: 0, attackDamage: 10);
        var service = GetCombatService();

        var result = service.ProcessPlayerAttack(player, monster);

        Assert.Equal(4, result.DamageDealtToPlayer);
        Assert.Equal(96, player.Health);
    }

    [Fact]
    public void ProcessPlayerAttack_EventsUseTheMonstersName()
    {
        var player = CreateCharacter(attackDamage: 10);
        var monster = new TestMonster(maxHealth: 100, attackDamage: 5);
        var service = GetCombatService();

        var result = service.ProcessPlayerAttack(player, monster);

        Assert.Contains(result.Events, e => e is DamageDealt hit && hit.AttackerName == "Hero" && hit.TargetName == "Test monster");
        Assert.Contains(result.Events, e => e is DamageDealt hit && hit.AttackerName == "Test monster" && hit.TargetName == "Hero");
    }
}