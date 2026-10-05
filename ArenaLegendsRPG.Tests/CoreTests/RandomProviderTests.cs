using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests;

public class RandomProviderTests
{
    [Fact]
    public void Next_ReturnsValuesInTheOrderTheyWereGiven()
    {
        var provider = new StubRandomProvider(5, 10, 3);
        Assert.Equal(5, provider.Next(0, 100));
        Assert.Equal(10, provider.Next(0, 100));
        Assert.Equal(3, provider.Next(0, 100));
    }
}

