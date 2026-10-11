using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.Fighting;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameServices;

namespace ArenaLegendsRPG.Tests.ServiceTests;

public class LevelingServiceTests
{
    private readonly ILevelingService _sut = new LevelingService();

    private static Character CreateCharacter()
    {
        return new Character("Hero", health: 100, baseAttackDamage: 10, baseMagicDamage: 5, baseAttackResist: 5, baseMagicResist: 2);
    }

    [Theory]
    [InlineData(1, 100)]
    [InlineData(2, 200)]
    [InlineData(5, 500)]
    public void XpRequiredForNextLevel_IsLevelTimesHundred(int level, int expected)
    {
        Assert.Equal(expected, _sut.XpRequiredForNextLevel(level));
    }

    [Fact]
    public void GainXp_NotEnoughForLevelUp_OnlyAddsXp()
    {
        var character = CreateCharacter();

        var events = _sut.GainXp(character, 50);

        Assert.Equal(1, character.Level);
        Assert.Equal(50, character.Xp);
        Assert.Contains(events, e => e is XpGained gained && gained.Amount == 50);
        Assert.DoesNotContain(events, e => e is LeveledUp);
    }

    [Fact]
    public void GainXp_EnoughForLevelUp_LevelsUpAndRaisesStats()
    {
        var character = CreateCharacter();
        var maxHealth = character.MaxHealth;
        var attackDamage = character.BaseAttackDamage;
        var magicDamage = character.BaseMagicDamage;
        var attackResist = character.BaseAttackResist;
        var magicResist = character.BaseMagicResist;

        var events = _sut.GainXp(character, 100);

        Assert.Equal(2, character.Level);
        Assert.Equal(0, character.Xp);
        Assert.Equal(maxHealth + 10, character.MaxHealth);
        Assert.Equal(attackDamage + 2, character.BaseAttackDamage);
        Assert.Equal(magicDamage + 1, character.BaseMagicDamage);
        Assert.Equal(attackResist + 1, character.BaseAttackResist);
        Assert.Equal(magicResist + 1, character.BaseMagicResist);
        Assert.Contains(events, e => e is LeveledUp leveled && leveled.NewLevel == 2);
    }

    [Fact]
    public void GainXp_LevelUp_HealsToFull()
    {
        var character = CreateCharacter();
        character.TakeDamage(new Damage(50, DamageType.Physical));

        _sut.GainXp(character, 100);

        Assert.Equal(character.MaxHealth, character.Health);
    }

    [Fact]
    public void GainXp_EnoughForSeveralLevels_LevelsUpRepeatedly()
    {
        var character = CreateCharacter();

        var events = _sut.GainXp(character, 350);

        Assert.Equal(3, character.Level);
        Assert.Equal(50, character.Xp);
        Assert.Equal(2, events.OfType<LeveledUp>().Count());
    }
}

