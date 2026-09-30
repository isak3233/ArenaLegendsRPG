using ArenaLegendsRPG.Core.Items;

namespace ArenaLegendsRPG.Tests;

public class ItemTests
{
    [Fact]
    public void Constructor_SetsNameAndDescription()
    {
        var item = new Item("Sword", "A sharp blade");

        Assert.Equal("Sword", item.Name);
        Assert.Equal("A sharp blade", item.Description);
    }
}