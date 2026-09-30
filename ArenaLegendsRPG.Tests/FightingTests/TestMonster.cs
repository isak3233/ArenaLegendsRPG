using ArenaLegendsRPG.Core.Monster;

namespace ArenaLegendsRPG.Tests.FightingTests;

public class TestMonster : MonsterBase
{
    public TestMonster(int maxHealth = 30, int armor = 2, int magicResistance = 0) : base(maxHealth, armor, magicResistance)
    {
    }
}