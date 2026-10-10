using ArenaLegendsRPG.Core.Fighting;
using ArenaLegendsRPG.Tests.TestDoubles;
using ArenaLegendsRPG.Core.GameFlow;



namespace ArenaLegendsRPG.Tests.CoreTests;

public class MonsterTests
{
    [Theory]
    [InlineData(30, 2, 5, 10, DamageType.Physical, 8, 22)]
    [InlineData(30, 2, 5, 10, DamageType.Magic, 5, 25)]
    [InlineData(30, 0, 0, 10, DamageType.Physical, 10, 20)]
    [InlineData(30, 10, 0, 5, DamageType.Physical, 0, 30)]
    public void TakeDamage_ReturnsActualDamage_AndReducesHealth(int maxHealth, int attackResist, int magicResist, int amount, DamageType type, int expectedDamage, int expectedHealth)
    {
        var monster = new TestMonster(maxHealth, attackResist, magicResist);

        var actual = monster.TakeDamage(new Damage(amount, type));

        Assert.Equal(expectedDamage, actual);
        Assert.Equal(expectedHealth, monster.Health);
    }

    [Fact]
    public void TakeDamage_HealthNeverGoesBelowZero()
    {
        var monster = new TestMonster(maxHealth: 10, attackResist: 0, magicResist: 0);

        monster.TakeDamage(new Damage(100, DamageType.Physical));

        Assert.Equal(0, monster.Health);
    }

    [Fact]
    public void TakeDamage_ReturnsFullCalculatedDamage_EvenWhenOverkill()
    {
        var monster = new TestMonster(maxHealth: 10, attackResist: 0, magicResist: 0);

        var actual = monster.TakeDamage(new Damage(100, DamageType.Physical));

        Assert.Equal(100, actual);
    }


    [Fact]
    public void TakeDamage_MultipleHits_AccumulateDamage()
    {
        var monster = new TestMonster(maxHealth: 30, attackResist: 2, magicResist: 0);

        monster.TakeDamage(new Damage(10, DamageType.Physical));
        monster.TakeDamage(new Damage(10, DamageType.Physical));

        Assert.Equal(14, monster.Health);
        Assert.False(monster.IsDead);
    }
    [Fact]
    public void XpReward_IsBasedOnHealthAndDamage()
    {
        var monster = new TestMonster(maxHealth: 30, attackDamage: 8, magicDamage: 2);

        Assert.Equal(80, monster.XpReward); // 30 + (8 + 2) * 5
    }

    [Fact]
    public void XpReward_StrongerMonsterGivesMoreXp()
    {
        var weak = new TestMonster(maxHealth: 20, attackDamage: 2, magicDamage: 0);
        var strong = new TestMonster(maxHealth: 50, attackDamage: 10, magicDamage: 5);

        Assert.True(strong.XpReward > weak.XpReward);
    }
}