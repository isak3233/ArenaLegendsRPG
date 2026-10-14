using ArenaLegendsRPG.Core.RandomGen;

namespace ArenaLegendsRPG.Tests.CoreTests;

public class RandomProviderTests
{
    [Fact]
    public void Next_AlwaysReturnsValueWithinRange()
    {
        var sut = new RandomProvider();

        for (var i = 0; i < 1000; i++)
        {
            var value = sut.Next(5, 10);

            Assert.True(value >= 5 && value < 10);
        }
    }

    [Fact]
    public void Next_RangeOfOne_ReturnsMin()
    {
        var sut = new RandomProvider();

        Assert.Equal(7, sut.Next(7, 8));
    }
}