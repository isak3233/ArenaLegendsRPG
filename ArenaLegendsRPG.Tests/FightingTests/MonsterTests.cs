using ArenaLegendsRPG.Core.Fighting;
using ArenaLegendsRPG.Core.Monster;

namespace ArenaLegendsRPG.Tests.FightingTests;

public class MonsterTests
{
    [Theory]
    [InlineData(30, 2, 5, 10, DamageType.Physical, 8, 22)]  
    [InlineData(30, 2, 5, 10, DamageType.Magic, 5, 25)]     
    [InlineData(30, 0, 0, 10, DamageType.Physical, 10, 20)] 
    [InlineData(30, 10, 0, 5, DamageType.Physical, 0, 30)]  
    public void TakeDamage_ReturnsActualDamage_AndReducesHealth(int maxHealth, int armor, int magicResistance, int amount, DamageType type, int expectedDamage, int expectedHealth)
    {
        var monster = new TestMonster(maxHealth, armor, magicResistance);

        var actual = monster.TakeDamage(new Damage(amount, type));

        Assert.Equal(expectedDamage, actual);
        Assert.Equal(expectedHealth, monster.Health);
    }

    [Fact]
    public void TakeDamage_HealthNeverGoesBelowZero()
    {
        var monster = new TestMonster(maxHealth: 10, armor: 0, magicResistance: 0);

        monster.TakeDamage(new Damage(100, DamageType.Physical));

        Assert.Equal(0, monster.Health);
    }

    [Fact]
    public void TakeDamage_ReturnsFullCalculatedDamage_EvenWhenOverkill()
    {
        var monster = new TestMonster(maxHealth: 10, armor: 0, magicResistance: 0);

        var actual = monster.TakeDamage(new Damage(100, DamageType.Physical));

        Assert.Equal(100, actual);
    }


    [Fact]
    public void TakeDamage_MultipleHits_AccumulateDamage()
    {
        var monster = new TestMonster(maxHealth: 30, armor: 2, magicResistance: 0);

        monster.TakeDamage(new Damage(10, DamageType.Physical)); 
        monster.TakeDamage(new Damage(10, DamageType.Physical)); 

        Assert.Equal(14, monster.Health);
        Assert.False(monster.IsDead);
    }

}