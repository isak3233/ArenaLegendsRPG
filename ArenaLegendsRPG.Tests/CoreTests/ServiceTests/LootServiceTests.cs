using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Core.Items;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests.ServiceTests;

public class LootServiceTests
{
    [Fact]
    public void GenerateLoot_FirstIndex_ReturnsRustySword()
    {
        var sut = new LootService(new StubRandomProvider(0, 0));

        var item = sut.GenerateLoot();

        var weapon = Assert.IsType<Weapon>(item);
        Assert.Equal("Rusty Sword", weapon.Name);
        Assert.Equal(3, weapon.AttackBonus);
        Assert.Equal(ItemRarity.Common, weapon.Rarity);
    }

    [Fact]
    public void GenerateLoot_ProtectionIndex_ReturnsProtection()
    {
        var sut = new LootService(new StubRandomProvider(3, 0));

        var item = sut.GenerateLoot();

        Assert.IsType<Protection>(item);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    public void GenerateLoot_EveryIndex_ReturnsNamedItem(int index)
    {
        var sut = new LootService(new StubRandomProvider(index, 0));

        var item = sut.GenerateLoot();

        Assert.False(string.IsNullOrWhiteSpace(item.Name));
    }

    [Fact]
    public void GenerateLoot_SameIndexTwice_ReturnsSeparateInstances()
    {
        var sut = new LootService(new StubRandomProvider(0, 0, 0, 0));

        var first = sut.GenerateLoot();
        var second = sut.GenerateLoot();

        Assert.NotSame(first, second);
    }

    [Theory]
    [InlineData(69, ItemRarity.Common, 3)]
    [InlineData(70, ItemRarity.Rare, 4)]
    [InlineData(90, ItemRarity.Epic, 5)]
    [InlineData(98, ItemRarity.Legendary, 6)]
    public void GenerateLoot_AppliesRarityAndScalesWeaponStats(
        int rarityRoll,
        ItemRarity expectedRarity,
        int expectedAttackBonus)
    {
        var sut = new LootService(new StubRandomProvider(0, rarityRoll));

        var weapon = Assert.IsType<Weapon>(sut.GenerateLoot());

        Assert.Equal(expectedRarity, weapon.Rarity);
        Assert.Equal(expectedAttackBonus, weapon.AttackBonus);
    }

    [Fact]
    public void RollMonsterDrop_LowRoll_ReturnsItem()
    {
        var sut = new LootService(new StubRandomProvider(10, 0, 0));

        var item = sut.RollMonsterDrop();

        Assert.Equal("Rusty Sword", item?.Name);
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(39, true)]
    [InlineData(40, false)]
    [InlineData(99, false)]
    public void RollMonsterDrop_RespectsDropChance(int roll, bool expectedDrop)
    {
        var sut = new LootService(new StubRandomProvider(roll, 0, 0));

        var item = sut.RollMonsterDrop();

        Assert.Equal(expectedDrop, item is not null);
    }

    [Fact]
    public void GenerateLoot_UnknownIndex_Throws()
    {
        var sut = new LootService(new StubRandomProvider(99));

        Assert.Throws<ArgumentOutOfRangeException>(() => sut.GenerateLoot());
    }
}