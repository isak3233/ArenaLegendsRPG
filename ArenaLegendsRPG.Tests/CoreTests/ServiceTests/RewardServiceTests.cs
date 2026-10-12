using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.Encounters;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Core.Items;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests.ServiceTests;

public class RewardServiceTests
{
    private readonly RewardService _sut = new(new LevelingService());

    private static Character CreatePlayer()
    {
        return new Character("Hero", health: 100, baseAttackDamage: 10, baseMagicDamage: 5, baseAttackResist: 5, baseMagicResist: 2);
    }

    private static MonsterEncounter CreateMonsterEncounter(int attackDamage = 8, int magicDamage = 2, Weapon? drop = null)
    {
        return new MonsterEncounter
        {
            Monster = new TestMonster(maxHealth: 30, attackDamage: attackDamage, magicDamage: magicDamage),
            Drop = drop
        };
    }

    [Fact]
    public void GrantMonsterRewards_GivesXpFromMonster()
    {
        var player = CreatePlayer();

        var events = _sut.GrantMonsterRewards(player, CreateMonsterEncounter()); 

        Assert.Equal(80, player.Xp);
        Assert.Contains(events, e => e is XpGained gained && gained.Amount == 80);
    }

    [Fact]
    public void GrantMonsterRewards_EnoughXp_LevelsUpPlayer()
    {
        var player = CreatePlayer();

        var events = _sut.GrantMonsterRewards(player, CreateMonsterEncounter(attackDamage: 10, magicDamage: 10)); 

        Assert.Equal(2, player.Level);
        Assert.Contains(events, e => e is LeveledUp);
    }

    [Fact]
    public void GrantMonsterRewards_WithDrop_AddsItemToInventory()
    {
        var player = CreatePlayer();
        var sword = new Weapon("Sword", "A sharp blade", attackBonus: 5);

        var events = _sut.GrantMonsterRewards(player, CreateMonsterEncounter(drop: sword));

        Assert.Same(sword, Assert.Single(player.Inventory.GetItems()));
        Assert.Contains(events, e => e is ItemFound found && found.PlayerName == "Hero" && found.ItemName == "Sword");
    }

    [Fact]
    public void GrantMonsterRewards_WithoutDrop_InventoryStaysEmpty()
    {
        var player = CreatePlayer();

        var events = _sut.GrantMonsterRewards(player, CreateMonsterEncounter());

        Assert.Empty(player.Inventory.GetItems());
        Assert.DoesNotContain(events, e => e is ItemFound);
    }

    [Fact]
    public void GrantChestLoot_AddsItemToInventory_AndReturnsItemFound()
    {
        var player = CreatePlayer();
        var sword = new Weapon("Sword", "A sharp blade", attackBonus: 5);
        var chest = new ChestEncounter { Loot = sword };

        var events = _sut.GrantChestLoot(player, chest);

        Assert.Same(sword, Assert.Single(player.Inventory.GetItems()));
        Assert.Contains(events, e => e is ItemFound found && found.ItemName == "Sword");
    }
}