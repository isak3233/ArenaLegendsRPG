using ArenaLegendsRPG.Core.Monster;

namespace ArenaLegendsRPG.Tests.TestDoubles;

internal class TestMonster : MonsterBase
{
    public TestMonster(int maxHealth = 30, int attackResist = 2, int magicResist = 0) : base(maxHealth, attackResist, magicResist)
    {
    }
}