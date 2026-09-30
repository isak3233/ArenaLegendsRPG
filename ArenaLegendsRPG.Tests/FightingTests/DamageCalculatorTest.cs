using ArenaLegendsRPG.Core.Fighting;

namespace ArenaLegendsRPG.Tests.FightingTests;

public class DamageCalculatorTest
{
    [Theory]
    [InlineData(10, DamageType.Physical, 2, 5, 8)]
    [InlineData(10, DamageType.Magic, 2, 5, 5)]
    [InlineData(10, DamageType.Physical, 0, 0, 10)]
    [InlineData(5, DamageType.Physical, 10, 0, 0)]
    [InlineData(5, DamageType.Magic, 0, 5, 0)]
    [InlineData(0, DamageType.Physical, 0, 0, 0)]
    public void Calculate_ReturnsExpectedDamage(int amount, DamageType type, int armor, int magicResistance, int expected)
    {
        var damage = new Damage(amount, type);

        var result = DamageCalculator.Calculate(damage, armor, magicResistance);

        Assert.Equal(expected, result);
    }
}