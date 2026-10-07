namespace ArenaLegendsRPG.Core.Monster;

public class SpiderMonster : MonsterBase
{
    public SpiderMonster(int maxHealth = 30, int attackResist = 2, int magicResist = 0, int attackDamage = 4, int magicDamage = 4)
        : base(maxHealth, attackResist, magicResist, attackDamage, magicDamage)
    {
    }
}