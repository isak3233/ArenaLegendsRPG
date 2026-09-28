using System;
using System.Collections.Generic;
using System.Text;

namespace ArenaLegendsRPG.Tests
{
    public class FakeRandomProviderTests
    {
        [Fact]
        public void Next_ReturnsValuesInTheOrderTheyWereGiven()
        {
            var provider = new FakeRandomProvider(5, 10, 3);
            Assert.Equal(5, provider.Next(0, 100));
            Assert.Equal(10, provider.Next(0, 100));
            Assert.Equal(3, provider.Next(0, 100));
        }
    }
}
