using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests.ServiceTests;

public class CombatServiceTests
{
    private static Character CreateCharacter(int health = 100, int attackDamage = 10, int attackResist = 0)
    {
        return new Character("Hero", health, attackDamage, baseMagicDamage: 0, attackResist, baseMagicResist: 0);
    }

    [Fact]
    public void ProcessPlayerAttack_MonsterSurvives_BothDealDamage()
    {
        var player = CreateCharacter(attackDamage: 10);
        var monster = new TestMonster(maxHealth: 30, attackResist: 0, magicResist: 0, attackDamage: 5);
        var service = new CombatService(new StubRandomProvider());

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
        var service = new CombatService(new StubRandomProvider());

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
        var service = new CombatService(new StubRandomProvider());

        var result = service.ProcessPlayerAttack(player, monster);

        Assert.Equal(8, result.DamageDealtToPlayer);  //Detta test failar för att den får 2 istället för 8, jag pallar inte idag tar det någon annan dag.
    }
    [Fact]
    public void AttemptFlee_LowRoll_Succeeds()
    {
        var random = new StubRandomProvider(10);
        var service = new CombatService(random);

        var result = service.AttemptFlee();

        Assert.True(result.Succeeded);
    }

    [Fact]
    public void AttemptFlee_HighRoll_Fails()
    {
        var random = new StubRandomProvider(80); 
        var service = new CombatService(random);

        var result = service.AttemptFlee();

        Assert.False(result.Succeeded);
    }
}